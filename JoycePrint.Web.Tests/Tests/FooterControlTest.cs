using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JoycePrint.Web.Tests.Tests
{
    /// <summary>
    /// Performs a simple login logout test and checks the users first, last and company name
    /// </summary>
    public class FooterControlTest : WebDriverTestBase
    {
        #region Base Properties & Functions

        protected override void RunTest(IWebDriver driver)
        {
            FooterPom = new FooterPom(driver);

            VerifyDisplay();
        }

        #endregion

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here
        /// </summary>
        private void VerifyDisplay()
        {
            FooterPom.FooterTestData.CopyrightText.MatchesActual(FooterPom.Copyright.Text, "Copyright Information");

            Wait1Sec.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(FooterPom.ByLogo)));
        }
    }
}