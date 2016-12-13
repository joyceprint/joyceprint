using System.Diagnostics.CodeAnalysis;
using JoycePrint.Web.Tests.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JoycePrint.Web.Tests.PageObjectModels
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class AboutUsPom : BasePom
    {
        /// <summary>
        /// The object containing all the test data required for the about us page
        /// </summary>
        public AboutUsTestData AboutUsTestData { get; set; }

        #region Selenium Properties

        /// <summary>
        /// The by form element for the page
        /// </summary>
        public static string ByForm => "[data-test-form-id='frmAboutUs']";

        /// <summary>
        /// The form element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-form-id='frmAboutUs']")]
        public IWebElement Form { get; set; }
        
        /// <summary>
        /// The parallax image element at the top of the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-parallax-top]")]
        public IWebElement ParallaxImageTop { get; set; }
        public static string ByParallaxImageTop => "[data-test-parallax-top]";

        /// <summary>
        /// The parallax image element in the middle of the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-parallax-middle]")]
        public IWebElement ParallaxImageMiddle { get; set; }
        public static string ByParallaxImageMiddle => "[data-test-parallax-middle]";

        /// <summary>
        /// The parallax image element at the bottom of the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-parallax-bottom]")]
        public IWebElement ParallaxImageBottom { get; set; }
        public static string ByParallaxImageBottom => "[data-test-parallax-bottom]";

        /// <summary>
        /// The company history image element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-company-history]")]
        public IWebElement CompanyHistory { get; set; }       

        /// <summary>
        /// The company history image element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-history-img]")]
        public IWebElement CompanyHistoryImage { get; set; }
        public static string ByCompanyHistoryImage => "[data-test-history-img]";
        
        /// <summary>
        /// The address label element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-address-label]")]
        public IWebElement AddressLabel { get; set; }
        
        /// <summary>
        /// The address text element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-address-text]")]
        public IWebElement AddressText { get; set; }        
             
        /// <summary>
        /// The phone label element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-phone-label]")]
        public IWebElement PhoneLabel { get; set; }

        /// <summary>
        /// The phone text element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-phone-text]")]
        public IWebElement PhoneText { get; set; }

        /// <summary>
        /// The fax label element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-fax-label]")]
        public IWebElement FaxLabel { get; set; }

        /// <summary>
        /// The fax text element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-fax-text]")]
        public IWebElement FaxText { get; set; }

        /// <summary>
        /// The email label element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-email-label]")]
        public IWebElement EmailLabel { get; set; }

        /// <summary>
        /// The email text element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-email-text]")]
        public IWebElement EmailText { get; set; }

        /// <summary>
        /// The skype label element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-skype-label]")]
        public IWebElement SkypeLabel { get; set; }
        
        /// <summary>
        /// The skype text element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-skype-text]")]
        public IWebElement SkypeText { get; set; }

        /// <summary>
        /// The map element for the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[data-test-map]")]
        public IWebElement Map { get; set; }
        public static string ByMap => "[data-test-map]";

        #endregion

        public AboutUsPom(IWebDriver driver) : base(driver, By.CssSelector(ByForm))
        {
            AboutUsTestData = new AboutUsTestData();
        }
    }
}