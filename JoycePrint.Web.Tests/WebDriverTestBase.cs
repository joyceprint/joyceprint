using System;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;

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
        /// A 20 second delay, used for controls with animations
        /// </summary>
        protected WebDriverWait Wait20Sec;

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
                Wait20Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
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
        /// Changes the screen size to the bounds specified for the enum value
        /// </summary>
        public void ResizeScreen(ScreenType screenType)
        {
            var screenSize = Supported.GetScreenSize(screenType);

            CurrentDriver.Manage().Window.Size = new Size(screenSize.Width, screenSize.Height);
        }

        #endregion       
    }
}
