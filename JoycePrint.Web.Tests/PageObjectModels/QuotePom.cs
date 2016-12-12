using JoycePrint.Web.Tests.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

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

        #region Quote Form Elements

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-banner-top]")]
        public IWebElement BannerTop { get; set; }

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-banner-bottom]")]
        public IWebElement BannerBottom { get; set; }

        /// <summary>
        /// The message input group form element for the page, the related items will be found using a utility function so we don't have to create them all here for each field
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-message]")]
        public IWebElement MessageInputGroup { get; set; }

        /// <summary>
        /// The recaptcha element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ data-test-recaptcha]")]
        public IWebElement Recaptcha { get; set; }

        /// <summary>
        /// The clear button element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-clear]")]
        public IWebElement Clear { get; set; }

        /// <summary>
        /// The submit button element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-submit]")]
        public IWebElement Submit { get; set; }

        #endregion

        #region Docket Help Partial View 

        /// <summary>
        /// A list of all the help titles on the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-help-title]")]
        public IList<IWebElement> HelpTitles { get; set; }


        /// <summary>
        /// The submit button element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-help-information]")]
        public IList<IWebElement> HelpInformation { get; set; }

        #endregion        

        #endregion

        public QuotePom(IWebDriver driver) : base(driver, By.CssSelector(ByForm))
        {
            QuoteTestData = new QuoteTestData();
        }
    }
}