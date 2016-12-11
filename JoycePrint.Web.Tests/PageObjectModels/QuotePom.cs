using JoycePrint.Web.Tests.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JoycePrint.Web.Tests.PageObjectModels
{
    public class QuotePom : BasePom
    {
        /// <summary>
        /// The object containing all the test data required for the quote page
        /// </summary>
        public QuoteTestData QuoteTestData { get; set; }

        #region Selenium Properties

        /// <summary>
        /// The by form element for the page
        /// </summary>
        public static string ByForm => "[data-test-form-id='frmQuote']";

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-form-id='frmHome']")]
        public IWebElement Form { get; set; }

        #endregion

        public QuotePom(IWebDriver driver) : base(driver, By.CssSelector(ByForm))
        {
            QuoteTestData = new QuoteTestData();
        }
    }
}