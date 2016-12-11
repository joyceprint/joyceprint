using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JoycePrint.Web.Tests.Tests
{
    /// <summary>
    /// Performs a simple login logout test and checks the users first, last and company name
    /// </summary>
    public class HeaderControlTest : WebDriverTestBase
    {
        #region Base Properties & Functions

        protected override void RunTest(IWebDriver driver)
        {
            HeaderPom = new HeaderPom(driver);

            VerifyDisplay();        
        }

        #endregion

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here        
        /// </summary>
        private void VerifyDisplay()
        {
            VerifyNavigation();

            VerifySideNavigation();
        }

        /// <summary>
        /// Verify the navigation control for the page
        /// </summary>
        private void VerifyNavigation()
        {
            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(HeaderPom.ByNavLogo)));
            Wait1Sec.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(HeaderPom.BySideNavLogo)));

            AssertAreEqual(HeaderPom.HeaderTestData.HomeText, HeaderPom.NavHome.Text, "Nav Home");
            AssertAreEqual(HeaderPom.HeaderTestData.QuoteText, HeaderPom.NavQuote.Text, "Nav Quote");
            AssertAreEqual(HeaderPom.HeaderTestData.AboutUsText, HeaderPom.NavAboutUs.Text, "Nav About Us");

            AssertAreEqual(HeaderPom.HeaderTestData.HomeLink, HeaderPom.NavHome.GetAttribute("href"), "Nav Home Link");
            AssertAreEqual(HeaderPom.HeaderTestData.QuoteLink, HeaderPom.NavQuote.GetAttribute("href"), "Nav Quote Link");
            AssertAreEqual(HeaderPom.HeaderTestData.AboutUsLink, HeaderPom.NavAboutUs.GetAttribute("href"), "Nav About Us Link");
        }

        /// <summary>
        /// Verify the side navigation control for the page
        /// </summary>
        /// <remarks>
        /// This is only present on small screen sizes, the browser window will have to be resized correctly for this function to work
        /// </remarks>
        private void VerifySideNavigation()
        {
            ResizeScreen(ScreenType.Tiny);

            Wait1Sec.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(HeaderPom.BySideNavLogo)));
            Wait1Sec.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(HeaderPom.ByNavLogo)));

            AssertAreEqual(HeaderPom.HeaderTestData.HomeText, HeaderPom.SideNavHome.Text, "Side Nav Home");
            AssertAreEqual(HeaderPom.HeaderTestData.QuoteText, HeaderPom.SideNavQuote.Text, "Side Nav Quote");
            AssertAreEqual(HeaderPom.HeaderTestData.AboutUsText, HeaderPom.SideNavAboutUs.Text, "Side Nav About Us");

            AssertAreEqual(HeaderPom.HeaderTestData.HomeLink, HeaderPom.SideNavHome.GetAttribute("href"), "Side Nav Home Link");
            AssertAreEqual(HeaderPom.HeaderTestData.QuoteLink, HeaderPom.SideNavQuote.GetAttribute("href"), "Side Nav Quote Link");
            AssertAreEqual(HeaderPom.HeaderTestData.AboutUsLink, HeaderPom.SideNavAboutUs.GetAttribute("href"), "Side Nav About Us Link");

            ResizeScreen(ScreenType.Large);
        }
    }
}
