using JoycePrint.Web.Tests.PageObjectModels;
using OpenQA.Selenium;

namespace JoycePrint.Web.Tests.Tests
{
    /// <summary>
    /// Performs a simple login logout test and checks the users first, last and company name
    /// </summary>
    public class HomePageTest : WebDriverTestBase
    {
        #region Base Properties & Functions

        protected override void RunTest(IWebDriver driver)
        {
            HomePom = new HomePom(driver);

            VerifyDisplay();
        }

        #endregion

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here
        /// </summary>
        private void VerifyDisplay()
        {
            // TODO:: Verify pressing the quote button gets you to the for,
        }
    }
}