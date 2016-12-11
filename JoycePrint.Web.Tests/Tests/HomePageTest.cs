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

        /// <summary>
        /// Test constructor        
        /// </summary>
        public HomePageTest()
        {            
        }

        protected override void RunTest(IWebDriver driver)
        {
            HomePom = new HomePom(driver);           
        }
        
        #endregion        
    }
}
