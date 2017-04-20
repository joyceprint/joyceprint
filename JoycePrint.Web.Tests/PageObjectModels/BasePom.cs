using System;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace JoycePrint.Web.Tests.PageObjectModels
{
    public class BasePom
    {
        /// <summary>
        /// 10 Second Wait - Use this through out the POM classes to pause while items are created
        /// </summary>
        protected WebDriverWait Wait10Sec { get; }

        /// <summary>
        /// 1 Second Wait - Use this through out the POM classes to pause while items are created
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        protected WebDriverWait Wait1Sec { get; }

        /// <summary>
        /// The web driver, this represents the browser
        /// </summary>
        protected IWebDriver Driver;

        /// <summary>
        /// Initializes the web driver for the browser, populates the POM models FindBy annotations
        /// and checks if the formId exists to indicate that the correct page has been loaded
        /// </summary>
        /// <param name="driver">The driver, this represents the browser</param>
        /// <param name="formId">The name of the form to check for</param>
        protected BasePom(IWebDriver driver, By formId)
        {
            Driver = driver;

            PageFactory.InitElements(driver, this);

            Wait10Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Wait1Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

            if (formId == null) return;

            Wait10Sec.Until(ExpectedConditions.ElementExists(formId));
        }

        /// <summary>
        /// This function is used to pause when buttons are clicked and we have no expected condition to watch for
        /// This is used in place of passing the driver around
        /// </summary>
        /// <param name="seconds"></param>
        public void Sleep(int seconds)
        {
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Resizes the browser window
        /// 
        /// Some button clicks are not being hit correctly due to the window opening at half width
        /// This would be fixed if the site was styled for mobile and table
        /// </summary>
        public void MaximizeWindow()
        {
            if (((RemoteWebDriver)Driver).Capabilities.BrowserName.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
            {
                var js = (IJavaScriptExecutor) Driver;
                js.ExecuteScript("window.resizeTo(1024, 768);");
            }
            else
            {
                Driver.Manage().Window.Maximize();
            }            
        }
    }
}