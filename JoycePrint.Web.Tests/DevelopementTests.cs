using System.Collections.Generic;
using JoycePrint.Web.Tests.Helpers;
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
            foreach (var browser in Supported.Browsers)
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
        [Description("UI test of the header control, including all navigation links and text, the size navigation is also checked")]
        public void HeaderControlTest()
        {
            TestRunner.RunTest<HeaderControlTest>(Drivers);
        }

        [TestCategory("Development")]
        [TestMethod]
        [Description("UI test of the footer control, including all links and text")]
        public void FooterControlText()
        {
            TestRunner.RunTest<FooterControlTest>(Drivers);
        }

        [TestCategory("Development")]
        [TestMethod]
        [Description("UI test of the home page, including all links and text")]
        public void HomePageTest()
        {
            TestRunner.RunTest<HomePageTest>(Drivers);
        }

        [TestCategory("Development")]
        [TestMethod]
        [Description("UI test of the quote page, ....")]
        public void QuotePageTest()
        {
            TestRunner.RunTest<QuotePageTest>(Drivers);
        }

        [TestCategory("Development")]
        [TestMethod]
        [Description("UI test of the about us page, including all links, text and images")]
        public void AboutUsPageTest()
        {
            TestRunner.RunTest<AboutUsPageTest>(Drivers);
        }
    }
}
