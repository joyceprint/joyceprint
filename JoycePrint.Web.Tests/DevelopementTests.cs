/*
 
 [1] FIX THE ABOUT US IMAGE FOR LARGE SCREENS

 [1] ADD LOGGING

 ******[1] UI TESTS 
      header
        check menu links text
        check image - logo url and alt text
        check navigation
            click on each menu link and ensure that the correct page is shown

     footer
        check the text
        check the links for phone and email

     home
        check the text
        check the image
        check the button brings you to the quote section

     service
        check the text
        check the images

     about us
        check the text
        check the image
        check the address, phone and email links - check the attributes are correct

        check the image is hidden when the screen is resized

     quote
        check the checklist text
        check the map section is present
        check the recaptcha is present

        check the clear button
        check validation
        check the submit buttom

        check the notifications
            failure
            success

 [2] ADD ERROR HANDLING
    add server error code pages
        and 404 not found page
    add general error pages

 [3] GET A GOOD FAVICON OFF CLAIRE

 [3] FIX UP THE CSS FILES

 [3] FIX UP THE JAVASCRIPT FILES

 [4] HANDLE THE JAVASCRIPT VALIDATION FOR LEGACY BROWSERS
 
 */
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
