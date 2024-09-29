using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using System;
using System.IO;

namespace EnsekSeleniumTest
{
    public class UnitTest1 : IDisposable
    {
        private IWebDriver driver;
        private StreamWriter logFile;
        private int totalTests;
        private int passedTests;
        private int failedTests;

        public UnitTest1()
        {
            // Initialize ChromeDriver
            driver = new ChromeDriver();
            // Initialize log file
            logFile = new StreamWriter("/Users/Manju/Downloads/ManjuAutomationTestPackage/EnsekSeleniumTest/EnsekSeleniumTestResults.txt", append: true);
            totalTests = 0;
            passedTests = 0;
            failedTests = 0;
        }

        [Fact]
        public void Test_Website_Title()
        {
            totalTests++;
            string screenshotPath = "/Users/Manju/Documents/EnsekSeleniumScreenshots/ErrorScreenshot_Title.png"; 
            string expectedTitle = "ENSEK Energy Corp. - Candidate Test";  // Updated to reflect actual title
            string actualTitle = string.Empty; 

            try
            {
                // Step 1: Launch the webpage
                driver.Navigate().GoToUrl("https://ensekautomationcandidatetest.azurewebsites.net/");
                LogMessage("Navigated to the home page.");
                
                // Step 2: Verify the title
                actualTitle = driver.Title;
                LogMessage($"Page title: {actualTitle}");

                // Assert Title
                Assert.Equal(expectedTitle, actualTitle);
                LogMessage("Title matches.");
                passedTests++; // Increment passed tests
            }
            catch (Exception ex)
            {
                LogError($"Error: Expected title '{expectedTitle}' but found '{actualTitle}'");
                TakeScreenshot(screenshotPath);
                failedTests++; // Increment failed tests
                throw new Exception("Title verification failed", ex); // Fail the test if title is incorrect
            }
            finally
            {
                LogSummary(); // Log a summary of the test results for this test
            }
        }

        [Fact]
        public void Test_Navigate_To_About_Page()
        {
            totalTests++;
            string screenshotPath = "/Users/Manju/Documents/EnsekSeleniumScreenshots/ErrorScreenshot_About.png"; // Path for the screenshot

            try
            {
                // Step 1: Launch the webpage
                driver.Navigate().GoToUrl("https://ensekautomationcandidatetest.azurewebsites.net/");
                LogMessage("Navigated to the home page.");

                // Step 2: Navigate to About page
                IWebElement aboutLink = WaitUntilClickable(By.LinkText("About"), TimeSpan.FromSeconds(10));
                aboutLink.Click();
                LogMessage("Navigated to the About page.");

                // Wait for the page to load and the button to be displayed
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => 
                {
                    try 
                    {
                        return driver.FindElement(By.XPath("//a[text()='Find out more about us »']")).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false; // If the element is not found yet
                    }
                });

                // Step 3: Click 'Find out more' button
                IWebElement findOutMoreButton = driver.FindElement(By.XPath("//a[text()='Find out more about us »']"));
                findOutMoreButton.Click();
                LogMessage("Clicked 'Find out more about us »' button. User successfully redirected.");

                // Log success in XUnit
                Assert.True(findOutMoreButton.Displayed, "Find out more button should be displayed.");
                LogMessage("Navigation to About page completed successfully.");
                passedTests++; // Increment passed tests
            }
            catch (StaleElementReferenceException ex)
            {
                LogError("Stale element reference encountered. Retrying to find the element...");
                TakeScreenshot(screenshotPath);
                failedTests++;
                throw new Exception("StaleElementReferenceException occurred", ex);
            }
            catch (Exception ex)
            {
                LogError($"Test failed during navigation or button click: {ex.Message}");
                TakeScreenshot(screenshotPath); // Capture a screenshot on failure
                failedTests++; // Increment failed tests
                throw; 
            }
            finally
            {
                LogSummary(); // Log a summary of the test results for this test
            }
        }

        // Helper method to wait until an element is clickable (without SeleniumExtras)
        private IWebElement WaitUntilClickable(By by, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            return wait.Until(driver =>
            {
                var element = driver.FindElement(by);
                return (element != null && element.Enabled && element.Displayed) ? element : null;
            });
        }

        // Helper method to capture screenshots
        private void TakeScreenshot(string filePath)
        {
            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("Path is null"));

                // Capture screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);  // No need to specify ScreenshotImageFormat

                LogMessage($"Screenshot captured: {filePath}");
            }
            catch (Exception ex)
            {
                LogError($"Failed to capture screenshot: {ex.Message}");
            }
        }

        // Helper method to log messages
        private void LogMessage(string message)
        {
            string logEntry = $"{DateTime.Now}: {message}";
            logFile.WriteLine(logEntry);
            Console.WriteLine(logEntry); // Print to console for real-time feedback
        }

        // Helper method to log errors
        private void LogError(string errorMessage)
        {
            string logEntry = $"{DateTime.Now} - ERROR: {errorMessage}";
            logFile.WriteLine(logEntry);
            Console.WriteLine(logEntry); // Print to console for real-time feedback
        }

        // Helper method to log a summary of the test results
        private void LogSummary()
        {
            string summary = $"{DateTime.Now} - Test Summary: Total Tests: {totalTests}, Passed: {passedTests}, Failed: {failedTests}";
            logFile.WriteLine(summary);
            Console.WriteLine(summary); // Print to console for real-time feedback
        }

        public void Dispose()
        {
            // Clean up resources: Close driver and log file
            driver.Quit();
            logFile.Close();
        }
    }
}
