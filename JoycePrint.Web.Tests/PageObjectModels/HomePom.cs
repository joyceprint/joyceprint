using JoycePrint.Web.Tests.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JoycePrint.Web.Tests.PageObjectModels
{
    public class HomePom : BasePom
    {
        /// <summary>
        /// The object containing all the test data required for the header
        /// </summary>
        public HomeTestData HomeTestData { get; set; }

        #region Selenium Properties

        /// <summary>
        /// The by form element for the page
        /// </summary>
        public static string ByForm => "[data-test-form-id='frmHome']";

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-form-id='frmHome']")]
        public IWebElement Form { get; set; }

        #endregion

        public HomePom(IWebDriver driver) : base(driver, By.CssSelector(ByForm))
        {
            HomeTestData = new HomeTestData();
        }
    }
}