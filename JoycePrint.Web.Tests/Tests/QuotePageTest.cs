using JoycePrint.Web.Tests.PageObjectModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            HeaderPom = new HeaderPom(driver);
            HeaderPom.NavQuote.Click();
            
            QuotePom = new QuotePom(driver);

            VerifyDisplay();
        }

        #endregion
        
        /// <summary>
        /// Verify the quote form fields that are displayed
        /// </summary>
        private void VerifyQuoteDisplay()
        {
            AssertAreEqual(QuotePom.QuoteTestData.BannerTopText, QuotePom.BannerTop.Text, "Top Banner");
            AssertAreEqual(QuotePom.QuoteTestData.BannerBottomText, QuotePom.BannerBottom.Text, "Bottom Banner");

            AssertAreEqual(QuotePom.QuoteTestData.ClearText, QuotePom.Clear.Text, "Clear Button");
            AssertAreEqual(QuotePom.QuoteTestData.SubmitText, QuotePom.Submit.Text, "Submit Button");

            Assert.IsTrue(QuotePom.Recaptcha.Displayed, "Recaptcha Missing");

            // Verify the message field
            VerifyMaterializeFieldOnLoad(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message);
        }

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here        
        /// </summary>
        private void VerifyDisplay()
        {
            VerifyQuoteDisplay();

            VerifyHelpDisplay();

            VerifyContactFormDisplay();

            VerifyProductFormDisplay();            
        }

        /// <summary>
        /// Verify the help that's displayed on the form
        /// </summary>
        private void VerifyHelpDisplay()
        {

        }

        /// <summary>
        /// Verify the contact form fields that are displayed
        /// </summary>
        private void VerifyContactFormDisplay()
        {

        }

        /// <summary>
        /// Verify the product form fields that are displayed
        /// </summary>
        private void VerifyProductFormDisplay()
        {

        }        
    }
}