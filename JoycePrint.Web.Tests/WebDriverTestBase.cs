using System;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;
using JoycePrint.Web.Tests.TestData;

namespace JoycePrint.Web.Tests
{
    public abstract class WebDriverTestBase
    {
        #region Abstract Properties & Functions

        /// <summary>
        /// This is the entry point for tests, each test needs to implement this function
        /// </summary>
        /// <param name="driver">The driver object for the browser</param>
        protected abstract void RunTest(IWebDriver driver);

        #endregion

        #region Concrete Properties

        /// <summary>
        /// The current web driver
        /// </summary>
        protected IWebDriver CurrentDriver;

        /// <summary>
        /// The Url to hit for the tests
        /// </summary>
        protected string Url { get; } = Urls.UrlDellDev;

        /// <summary>
        /// A 10 second delay
        /// </summary>
        protected WebDriverWait Wait10Sec;

        /// <summary>
        /// A 1 second delay
        /// </summary>
        protected WebDriverWait Wait1Sec;

        /// <summary>
        /// Gets the name of the test being run
        /// </summary>
        public string Name
        {
            get { return GetType().Name; }
        }

        #endregion

        #region POM Objects

        /// <summary>
        /// Common POM Objects that will be used throughout the application
        /// </summary>
        public HomePom HomePom;
        public QuotePom QuotePom;
        public AboutUsPom AboutUsPom;

        public HeaderPom HeaderPom;
        public FooterPom FooterPom;

        #endregion

        /// <summary>
        /// Runs the tests on the browsers repersented in the drivers object
        /// </summary>
        /// <param name="drivers">A enumberable list of drivers to use</param>
        public void Run(IEnumerable<IWebDriver> drivers)
        {
            foreach (var driver in drivers)
            {
                CurrentDriver = driver;

                Assert.IsNotNull(driver);
                Wait10Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                Wait1Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                Go(driver);
                RunInternal(driver);
            }
        }

        /// <summary>
        /// This is the entry point for tests, we handle catching exceptions here that are thrown during the test
        /// </summary>
        /// <param name="driver">The driver object for the browser</param>
        protected void RunInternal(IWebDriver driver)
        {
            Exception caughtException = null;

            try
            {
                RunTest(driver);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }
            finally
            {
                //driver.Close();
                //driver.Quit();
                //driver.Dispose();
            }

            if (caughtException != null)
            {
                throw caughtException;
            }
        }

        /// <summary>
        /// Navigate to the url using the browser driver
        /// </summary>
        /// <param name="driver"></param>
        private void Go(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(Url);

            // Maximize the window here so it's done for every test
            driver.Manage().Window.Maximize();
        }

        #region Helper Functions

        /// <summary>
        /// Wait for a period of time
        /// </summary>
        /// <param name="delay">The wait time in milli seconds</param>
        /// <param name="interval">The interval to check the clock</param>
        /// <param name="driver">The web driver object</param>
        public void Wait(double delay, double interval, IWebDriver driver)
        {
            // Causes the WebDriver to wait for at least a fixed delay
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(delay))
            {
                PollingInterval = TimeSpan.FromMilliseconds(interval)
            };

            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromMilliseconds(delay) > TimeSpan.Zero);
        }

        /// <summary>
        /// Waits until the popup window has appeared
        /// This will check for 2 window handles
        /// </summary>
        public void WaitForPopup()
        {
            Wait1Sec.Until((d) => (d.WindowHandles.Count == 2));
        }

        /// <summary>
        /// Asserts that the expected and actual values match
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The actual string</param>
        /// <param name="field">The field being tested</param>
        public void AssertAreEqual(string expected, string actual, string field)
        {
            Assert.AreEqual(expected, actual, $"The expected {field} text [{expected}] differs from the actual {field} text [{actual}]");
        }

        /// <summary>
        /// Changes the screen size to the bounds specified for the enum value
        /// </summary>
        /// <param name="screenSize"></param>
        public void ResizeScreen(ScreenType screenType)
        {
            var screenSize = Supported.GetScreenSize(screenType);

            CurrentDriver.Manage().Window.Size = new Size(screenSize.Width, screenSize.Height);
        }

        #endregion

        #region Materialize Functions

        /// <summary>
        /// Verfiy the materialize fields state when the page is loaded       
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="testData">The test data to be used for the comparision</param>
        public void VerifyMaterializeFieldOnLoad(IWebElement inputGroupContainer, MaterializeTestData testData)
        {
            IWebElement iconElement = null;
            IWebElement inputElement = null;
            IWebElement labelElement = null;
            IWebElement validationLabelElement = null;
            string fieldName = null;

            GetMaterializeWebElements(inputGroupContainer, ref iconElement, ref inputElement, ref labelElement, ref validationLabelElement, ref fieldName, "textarea");

            AssertAreEqual(testData.IconClasses, iconElement.GetAttribute("class"), $"{fieldName} Icon Classes");
            AssertAreEqual(testData.IconText, iconElement.Text, $"{fieldName} Icon Text");

            AssertAreEqual(testData.InputClasses, inputElement.GetAttribute("class"), $"{fieldName} Input Classes");

            if (testData.InputText != null)
                AssertAreEqual(testData.InputText, inputElement.Text, $"{fieldName} Input Text");

            if (testData.LabelClasses != null)
                AssertAreEqual(testData.LabelClasses, labelElement.GetAttribute("class"), $"{fieldName} Label Classes");

            if (testData.LabelText != null)
                AssertAreEqual(testData.LabelText, labelElement.Text, $"{fieldName} Label Text");

            if (null == validationLabelElement) return;

            AssertAreEqual(testData.ValidationLabelClasses, validationLabelElement.GetAttribute("class"), $"{fieldName} Validation Label Classes");

            // We only check the validation if it's displayed
            if (validationLabelElement.Displayed)
                AssertAreEqual(testData.ValidationLabelText, validationLabelElement.Text, $"{fieldName} Validation Label Text");
        }

        /// <summary>
        /// This method gets the input group fields for the inputGroupContainer passed in
        /// The input arguments are passed in as references so we don't have to use globals or multiple returns
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="iconElement">The icon element will be stored here</param>
        /// <param name="inputElement">The input element will be stored here</param>
        /// <param name="labelElement">The label element will be stored here</param>
        /// <param name="validationLabelElement">The validation label element will be stored here</param>
        /// <param name="inputTag">The input tag name to look for when setting the input element</param>
        public void GetMaterializeWebElements(IWebElement inputGroupContainer, ref IWebElement iconElement, ref IWebElement inputElement, ref IWebElement labelElement, ref IWebElement validationLabelElement, ref string fieldName, string inputTag)
        {
            iconElement = inputGroupContainer.FindElement(By.TagName("i"));
            inputElement = inputGroupContainer.FindElement(By.TagName(inputTag));

            var labelElements = inputGroupContainer.FindElements(By.TagName("label"));
            labelElement = labelElements[0];

            fieldName = labelElement.Text;

            if (labelElements.Count != 2) return;
            validationLabelElement = labelElements[1];
        }

        #endregion
    }
}
