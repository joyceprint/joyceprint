using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JoycePrint.Web.Tests.Tests
{
    /// <summary>
    /// Test the about us page including all text and links.
    /// The images are checked by their visibility
    /// The application links are also check for correctness, but not functionality
    /// </summary>
    public class AboutUsPageTest : WebDriverTestBase
    {
        #region Base Properties & Functions

        protected override void RunTest(IWebDriver driver)
        {
            HeaderPom = new HeaderPom(driver);
            HeaderPom.NavAboutUs.Click();

            AboutUsPom = new AboutUsPom(driver);

            VerifyDisplay();        
        }
        
        #endregion        

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here
        /// </summary>
        private void VerifyDisplay()
        {
            VerifyParallaxImages();

            VerifyCompanyHistory();

            VerifyCompanyInformation();

            VerifyAnchorLinksToApplications();
        }

        /// <summary>
        /// Verify that the images used for parallax are being displayed
        /// </summary>
        private void VerifyParallaxImages()
        {
            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByParallaxImageTop)));
            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByParallaxImageMiddle)));
            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByParallaxImageBottom)));
        }

        /// <summary>
        /// Verify the company history section has the correct text and the image is being displayed
        /// </summary>
        private void VerifyCompanyHistory()
        {
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.CompanyHistoryText, AboutUsPom.CompanyHistory.Text, "Company History");
            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByCompanyHistoryImage)));
        }

        /// <summary>
        /// Verify the company information section has the correct text and the map from google is being displayed
        /// </summary>
        private void VerifyCompanyInformation()
        {
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.AddressLabel, AboutUsPom.AddressLabel.Text, "Address Label");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.AddressText, AboutUsPom.AddressText.Text, "Address Text");

            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.PhoneLabel, AboutUsPom.PhoneLabel.Text, "Phone Label");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.PhoneText, AboutUsPom.PhoneText.Text, "Phone Text");

            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.FaxLabel, AboutUsPom.FaxLabel.Text, "Fax Label");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.FaxText, AboutUsPom.FaxText.Text, "Fax Text");

            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.EmailLabel, AboutUsPom.EmailLabel.Text, "Email Label");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.EmailText, AboutUsPom.EmailText.Text, "Email Text");

            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.SkypeLabel, AboutUsPom.SkypeLabel.Text, "Skype Label");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.SkypeText, AboutUsPom.SkypeText.Text, "Skype Text");

            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByMap)));
        }

        /// <summary>
        /// Verify the anchor tags have the correct href information based on their type
        /// </summary>
        private void VerifyAnchorLinksToApplications()
        {
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.PhoneLink, AboutUsPom.PhoneText.GetAttribute("href"), "Phone Link");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.EmailLink, AboutUsPom.EmailText.GetAttribute("href"), "Email Link");
            AssertHelper.AssertAreEqual(AboutUsPom.AboutUsTestData.SkypeLink, AboutUsPom.SkypeText.GetAttribute("href"), "Skype Link");
        }
    }
}
