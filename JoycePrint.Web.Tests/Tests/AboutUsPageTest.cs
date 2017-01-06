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
            AboutUsPom.AboutUsTestData.CompanyHistoryText.MatchesActual(AboutUsPom.CompanyHistory.Text, "Company History");

            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByCompanyHistoryImage)));
        }

        /// <summary>
        /// Verify the company information section has the correct text and the map from google is being displayed
        /// </summary>
        private void VerifyCompanyInformation()
        {
            AboutUsPom.AboutUsTestData.AddressLabel.MatchesActual(AboutUsPom.AddressLabel.Text, "Address Label");
            AboutUsPom.AboutUsTestData.AddressText.MatchesActual(AboutUsPom.AddressText.Text, "Address Text");

            AboutUsPom.AboutUsTestData.PhoneLabel.MatchesActual(AboutUsPom.PhoneLabel.Text, "Phone Label");
            AboutUsPom.AboutUsTestData.PhoneText.MatchesActual(AboutUsPom.PhoneText.Text, "Phone Text");

            AboutUsPom.AboutUsTestData.FaxLabel.MatchesActual(AboutUsPom.FaxLabel.Text, "Fax Label");
            AboutUsPom.AboutUsTestData.FaxText.MatchesActual(AboutUsPom.FaxText.Text, "Fax Text");

            AboutUsPom.AboutUsTestData.EmailLabel.MatchesActual(AboutUsPom.EmailLabel.Text, "Email Label");
            AboutUsPom.AboutUsTestData.EmailText.MatchesActual(AboutUsPom.EmailText.Text, "Email Text");

            AboutUsPom.AboutUsTestData.SkypeLabel.MatchesActual(AboutUsPom.SkypeLabel.Text, "Skype Label");
            AboutUsPom.AboutUsTestData.SkypeText.MatchesActual(AboutUsPom.SkypeText.Text, "Skype Text");

            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AboutUsPom.ByMap)));
        }

        /// <summary>
        /// Verify the anchor tags have the correct href information based on their type
        /// </summary>
        private void VerifyAnchorLinksToApplications()
        {
            AboutUsPom.AboutUsTestData.PhoneLink.MatchesActual(AboutUsPom.PhoneText.GetAttribute("href"), "Phone Link");
            AboutUsPom.AboutUsTestData.EmailLink.MatchesActual(AboutUsPom.EmailText.GetAttribute("href"), "Email Link");
            AboutUsPom.AboutUsTestData.SkypeLink.MatchesActual(AboutUsPom.SkypeText.GetAttribute("href"), "Skype Link");
        }
    }
}
