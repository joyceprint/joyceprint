using JoycePrint.Web.Tests.PageObjectModels;
using OpenQA.Selenium;

namespace JoycePrint.Web.Tests.Tests
{
    /// <summary>
    /// Performs a simple login logout test and checks the users first, last and company name
    /// </summary>
    public class QuotePageTest : WebDriverTestBase
    {
        #region Base Properties & Functions

        protected override void RunTest(IWebDriver driver)
        {
            QuotePom = new QuotePom(driver);

            VerifyDisplay();
        }

        #endregion

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here        
        /// </summary>
        private void VerifyDisplay()
        {
            
        }
    }
}
