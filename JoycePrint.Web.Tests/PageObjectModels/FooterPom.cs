using JoycePrint.Web.Tests.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JoycePrint.Web.Tests.PageObjectModels
{
    public class FooterPom : BasePom
    {
        /// <summary>
        /// The object containing all the test data required for the footer
        /// </summary>
        public FooterTestData FooterTestData { get; set; }

        #region Selenium Properties

        /// <summary>
        /// The by form element for the page
        /// </summary>
        public static string ByForm => "[data-test-form-id='frmFooter']";

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-form-id='frmFooter']")]
        public IWebElement Form { get; set; }

        /// <summary>
        /// The image logo element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-logo]")]
        public IWebElement Logo { get; set; }
        public static string ByLogo => "[data-test-form-id='frmFooter']";

        /// <summary>
        /// The copyright text element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-copyright]")]
        public IWebElement Copyright { get; set; }

        #endregion

        public FooterPom(IWebDriver driver) : base(driver, By.CssSelector(ByForm))
        {
            FooterTestData = new FooterTestData();
        }
    }
}