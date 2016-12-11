using System.Collections.Generic;
using JoycePrint.Web.Tests.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace JoycePrint.Web.Tests
{
    [TestClass]
    public sealed class DevelopmentTests
    {
        private static readonly List<IWebDriver> Drivers = new List<IWebDriver>();
        
        #region Setup / Tear-down

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            foreach (var browser in SupportedBrowsers.Browsers)
                Drivers.Add(DriverSelector.GetWebDriver(browser, null));
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            foreach (var driver in Drivers)
            {
                driver.Close();
                driver.Quit();
                driver.Dispose();
            }
        }

        #endregion

        [TestCategory("Development")]
        [TestMethod]
        [Description("UI test of the home page, including all links and text")]
        public void HomePageTest()
        {
            TestRunner.RunTest<HomePageTest>(Drivers);
        }
    }
}
