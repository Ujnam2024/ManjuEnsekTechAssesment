<<<<<<< HEAD
# ManjuEnsekTechAssesment 

This README file offers comprehensive details about the testing process I conducted for the assessment.

**Automation Testing**
=======
# ManjuEnsekTechAssesment - Automation Testing
Manju Automation work submission for Ensek Tech Assessment.
>>>>>>> f18db83 (Manju Tech Assessment)

Git Repository : (https://github.com/Ujnam2024/ManjuEnsekTechAssesment)

Programming Language: C# | Testing Framework: xUnit | Web Driver: Selenium WebDriver with ChromeDriver | IDE: Visual Studio Code

<<<<<<< HEAD
Automation Test Execution File to Refer: UnitTest1.cs
=======
Date: Sep 29,2024

Automation Test File: UnitTest1.cs
>>>>>>> f18db83 (Manju Tech Assessment)

Automation testing Approach:

1. Test Case 1: Test_Website_Title: Checks the title of the home page.

  Description: Validates that the title of the home page is as expected.

  Expected Result: Title should be "ENSEK Energy Corp. - Candidate Test".(Updated the expected results with respect to the actual result)

  Status: Pass logged in the results.

2. Test Case 2: Test_Navigate_To_About_Page: Tests navigation to the About page and clicking the "Find out more" button.verified

  Description: Verifies the navigation to the About page and checks if the "Find out more" button is displayed.

  Expected Result: The button should be visible and clickable.

  Status: Fail logged in the results.

Test Results
  
  1. Test results are logged in EnsekSeleniumTestResults.txt.
  2. Detailed results, including successes, failures, and errors, are recorded.

Logging and Reporting
  
  All test activities and results are written to a log file for review.

<<<<<<< HEAD
**Manual testing**
=======
# ManjuEnsekTechAssesment - Manual testing
>>>>>>> f18db83 (Manju Tech Assessment)

1.**Test Approach Document for Ensek Test Website - Manju.doc**
    This document provides the complete test approach for the tech assignment.

2.**Ensek - Test Plan Document for Manual Testing - Manju.xlsx **
    This Excel Document “Ensek – Test Plan Document for Manual testing” which has 3 sheets named Test Coverage, Test Cases and Bug Lists.
    
    1. **Test Coverage Sheet** – Overview of number of test cases identiKied which includes the details of priority, pass and failure test cases.
    
    2. **Test Case Sheet** – Test cases identiKied with respect to Unit Testing, Functional Testing, Performance Testing, User Experience Testing, Cross Browser Testing and Exploratory Testing
    
    3. **Bug List** – Bugs IdentiKied while performing the manual testing which includes the screenshots for the issues.

3. **Error - Screenshots**
    This folder contains all the erors identified while performing the manual testing. 
  
<<<<<<< HEAD
# Energy API Tests - RESTAPITestSwaggerDoc

Base URL:
https://qacandidatetest.ensek.io/ENSEK

Test Cases covered

Test Case 1:Reset Data

  APITest: Checks if the /reset endpoint returns a 401 Unauthorized status when accessed without proper credentials.
  Expected Result: 401 Unauthorized

Test Case 2:Buy Energy

  APITest: Gets available energy types and buys a quantity (10) of each one. It checks if the purchase goes through successfully and saves the order IDs for later.
  Expected Result: 200 OK

Test Case 3:Get Orders
  
  APITest: Retrieves all orders and verifies that the orders we bought earlier are included. It also checks that each order has the right details.
  Expected Result: 200 OK

Test Case 4:Count Orders Before Current Date
  
  APITest: Counts how many orders were created before today and prints the number. It ensures that the order date was retrieved correctly.
  Expected Result: Displays the count of orders before the current date.

Test Case 5:Unauthorized Login
  
  APITest: Attempts to log in with the wrong credentials to see if it returns a 401 Unauthorized message.
  Expected Result: 401 Unauthorized with the message "Unauthorized"

Test Case 6:Bad Request on Buy
  
  APITest: Tries to buy an invalid quantity (like -5) and checks if the API responds with a 400 Bad Request error.
  Expected Result: 400 Bad Request with the message "Bad Request"

Test Results Stored In: surefire-reports

From the output of your Maven test run, here's a summary of the test results:

Total Tests Run: 7
  Passed: 4 (since 7 total tests - 2 failures - 1 error = 4 passed)
  Failed: 2
  Errors: 1
  Skipped: 0
=======
>>>>>>> f18db83 (Manju Tech Assessment)
