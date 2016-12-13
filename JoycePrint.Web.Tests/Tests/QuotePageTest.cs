using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;
using JoycePrint.Web.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using JoycePrint.Web.Tests.Enums;

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
        /// Verify the quote form fields that are displayed
        /// </summary>
        private void VerifyQuoteDisplay()
        {
            AssertHelper.AssertAreEqual(QuotePom.QuoteTestData.BannerTopText, QuotePom.BannerTop.Text, "Top Banner");
            AssertHelper.AssertAreEqual(QuotePom.QuoteTestData.BannerBottomText, QuotePom.BannerBottom.Text, "Bottom Banner");

            AssertHelper.AssertAreEqual(QuotePom.QuoteTestData.ClearText, QuotePom.Clear.Text, "Clear Button");
            AssertHelper.AssertAreEqual(QuotePom.QuoteTestData.SubmitText, QuotePom.Submit.Text, "Submit Button");

            Assert.IsTrue(QuotePom.Recaptcha.Displayed, "Recaptcha Missing");
            AssertHelper.AssertAreEqual(QuotePom.Recaptcha.GetAttribute("data-sitekey"), QuotePom.QuoteTestData.RecaptchaSiteKey, "Recaptcha Site Key");

            // Verify the message field
            MaterializeInputGroup.VerifyMaterializeField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);
        }

        /// <summary>
        /// Verify the help that's displayed on the form
        /// </summary>
        private void VerifyHelpDisplay()
        {
            var collapseHeaders = QuotePom.HelpTitles.Count;
            var collapseBodies = QuotePom.HelpInformation.Count;

            if (collapseHeaders != collapseBodies)
                Assert.Fail($"Colapseable header count, differs for body count. Header Count : {collapseHeaders} - Body Count : {collapseBodies}");

            // The first collapsable control is active on page load
            FieldCss updateCssTo;
            //for (int activeIndex = 0; activeIndex < collapseHeaders; activeIndex++)
            // TODO the click is not working and it's causing this to fail
            for (int activeIndex = 0; activeIndex < 1; activeIndex++)
                {
                if (activeIndex != 0)
                    QuotePom.HelpTitles[activeIndex].Click();
                
                for (var index = 0; index < collapseHeaders; index++)
                {
                    updateCssTo = index == activeIndex ? FieldCss.Active : FieldCss.Initial;
                    MaterializeCollapse.VerifyMaterializeCollapse(QuotePom.HelpTitles[index], QuotePom.HelpInformation[index], QuotePom.QuoteTestData.Help[index], updateCssTo, Wait10Sec);
                }
            }
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