package com.example;

import io.restassured.RestAssured;
import io.restassured.http.ContentType;
import io.restassured.response.Response;
import org.junit.BeforeClass;
import org.junit.Test;

import java.util.List;
import java.util.ArrayList;

import static io.restassured.RestAssured.*;
import static org.hamcrest.Matchers.*;

public class EnergyApiTests {

    private static final String BASE_URL = "https://qacandidatetest.ensek.io/ENSEK";
    private List<Integer> orderIds = new ArrayList<>();
    private String orderDate;

    @BeforeClass
    public static void setup() {
        RestAssured.baseURI = BASE_URL;
    }

    @Test
    public void testResetData() {
        given()
            .when()
            .post("/reset")
            .then()
            .statusCode(401);
    }

    @Test
    public void testBuyEnergy() {
        // Get the energy types
        Response energyResponse = given()
            .when()
            .get("/api/energy-ids")
            .then()
            .statusCode(200)  // Expecting a 200 status code for success
            .extract()
            .response();

        // Extract energy IDs
        List<Integer> energyIds = energyResponse.jsonPath().getList("id");

        // Check if the energy IDs list is empty or null
        if (energyIds == null || energyIds.isEmpty()) {
            throw new IllegalStateException("Failed to retrieve energy IDs.");
        }

        // Buy a quantity of each energy type
        for (Integer energyId : energyIds) {
            Response buyResponse = given()
                .pathParam("id", energyId)
                .pathParam("quantity", 10) // Example quantity
                .when()
                .put("/buy/{id}/{quantity}")
                .then()
                .statusCode(200) // Ensure this is the expected response
                .extract()
                .response();

            Integer orderId = buyResponse.jsonPath().getInt("orderId");
            orderIds.add(orderId); // Store the order ID for later verification
        }
    }


    @Test
    public void testGetOrders() {
    Response ordersResponse = given()
        .when()
        .get("/orders")
        .then()
        .statusCode(200)
        .extract()
        .response();

    // Retrieve and set order date from the response if needed
    if (ordersResponse.jsonPath().getList("creationDate") != null && !ordersResponse.jsonPath().getList("creationDate").isEmpty()) {
        orderDate = ordersResponse.jsonPath().getString("creationDate[0]"); // Adjust according to your needs
    }

    // Verify that each order from the previous step is returned
    for (Integer orderId : orderIds) {
        ordersResponse.then()
            .body("id", hasItem(orderId)); 
    }

    // Verify that each order has expected details
    for (Integer orderId : orderIds) {
        given()
            .pathParam("orderId", orderId)
            .when()
            .get("/orders/{orderId}")
            .then()
            .statusCode(200)
            .body("id", equalTo(orderId)) 
            .body("quantity", equalTo(10)); 
    }
    }

    @Test
    public void testCountOrdersBeforeCurrentDate() {
        Response ordersResponse = given()
            .when()
            .get("/orders")
            .then()
            .statusCode(200)
            .extract()
            .response();

        // Ensure orderDate is retrieved before counting
        if (orderDate == null) {
            throw new IllegalStateException("Failed to retrieve order date.");
        }

        // Iterate through orders to count how many were created before the current date
        List<String> orderDates = ordersResponse.jsonPath().getList("creationDate"); 
        String currentDate = java.time.LocalDateTime.now().toString();
        
        int countBeforeDate = 0;
        for (String orderDate : orderDates) {
            if (orderDate.compareTo(currentDate) < 0) {
                countBeforeDate++;
            }
        }
        
        System.out.println("Orders created before current date: " + countBeforeDate);
    }

    @Test
    public void testUnauthorizedLogin() {
        // Test for unauthorized login
        String payload = "{\"username\":\"wrongUser\", \"password\":\"wrongPassword\"}";
        
        given()
            .contentType(ContentType.JSON)
            .body(payload)
            .when()
            .post("/login")
            .then()
            .statusCode(401)
            .body("message", equalTo("Unauthorized")); 
    }

    @Test
    public void testBadRequestOnBuy() {
        // Attempt to buy an invalid quantity
        given()
            .pathParam("id", 1) 
            .pathParam("quantity", -5) 
            .when()
            .put("/buy/{id}/{quantity}")
            .then()
            .statusCode(400) // Expecting 400 for bad request
            .body("message", equalTo("Bad Request")); 
    }
}
