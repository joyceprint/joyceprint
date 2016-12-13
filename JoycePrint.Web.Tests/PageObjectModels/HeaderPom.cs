using System.Diagnostics.CodeAnalysis;
using JoycePrint.Web.Tests.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JoycePrint.Web.Tests.PageObjectModels
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class HeaderPom : BasePom
    {
        /// <summary>
        /// The object containing all the test data required for the header
        /// </summary>
        public HeaderTestData HeaderTestData { get; set; }

        #region Selenium Properties

        /// <summary>
        /// The by form element for the page
        /// </summary>
        public static string ByForm => "[data-test-form-id='frmHeader']";

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-form-id='frmHeader']")]
        public IWebElement Form { get; set; }

        #region Navigation Elements

        /// <summary>
        /// The navigation container element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-nav]")]
        public IWebElement Nav { get; set; }

        /// <summary>
        /// The navigation image logo element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-nav-logo]")]
        public IWebElement NavLogo { get; set; }
        public string ByNavLogo = "[data-test-nav-logo]";

        /// <summary>
        /// The navigation home link element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-nav-home]")]
        public IWebElement NavHome { get; set; }

        /// <summary>
        /// The navigation quote link element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-nav-quote]")]
        public IWebElement NavQuote { get; set; }

        /// <summary>
        /// The navigation about us link element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-nav-aboutus]")]
        public IWebElement NavAboutUs { get; set; }

        #endregion

        #region Side Navigation Elements
        
        /// <summary>
        /// The side navigation container element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-side-nav]")]
        public IWebElement SideNav { get; set; }

        /// <summary>
        /// The side navigation container element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-side-nav-menu]")]
        public IWebElement SideNavMenu { get; set; }

        /// <summary>
        /// The side navigation image logo element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-side-nav-logo]")]
        public IWebElement SideNavLogo { get; set; }
        public string BySideNavLogo = "[data-test-side-nav-logo]";

        /// <summary>
        /// The side navigation home link element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-side-nav-home]")]
        public IWebElement SideNavHome { get; set; }

        /// <summary>
        /// The side navigation quote link element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-side-nav-quote]")]
        public IWebElement SideNavQuote { get; set; }

        /// <summary>
        /// The side navigation about us link element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-side-nav-aboutus]")]
        public IWebElement SideNavAboutUs { get; set; }

        #endregion

        #endregion

        public HeaderPom(IWebDriver driver) : base(driver, By.CssSelector(ByForm))
        {
            HeaderTestData = new HeaderTestData();
        }
    }
}