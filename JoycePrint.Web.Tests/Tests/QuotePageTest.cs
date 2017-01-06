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

            //VerifyValidation();
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
            QuotePom.QuoteTestData.BannerTopText.MatchesActual(QuotePom.BannerTop.Text, "Top Banner");
            QuotePom.QuoteTestData.BannerBottomText.MatchesActual(QuotePom.BannerBottom.Text, "Bottom Banner");

            QuotePom.QuoteTestData.ClearText.MatchesActual(QuotePom.Clear.Text, "Clear Button");
            QuotePom.QuoteTestData.SubmitText.MatchesActual(QuotePom.Submit.Text, "Submit Button");

            Assert.IsTrue(QuotePom.Recaptcha.Displayed, "Recaptcha Missing");
            QuotePom.Recaptcha.GetAttribute("data-sitekey").MatchesActual(QuotePom.QuoteTestData.RecaptchaPublicKey, "Recaptcha Public Key");

            // Verify the message field
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);
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

            for (int activeIndex = 0; activeIndex < collapseHeaders; activeIndex++)
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
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.PositionInputGroup, QuotePom.QuoteTestData.Position, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.NameInputGroup, QuotePom.QuoteTestData.Name, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.PhoneInputGroup, QuotePom.QuoteTestData.Phone, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.EmailInputGroup, QuotePom.QuoteTestData.Email, FieldCss.Initial);
        }

        /// <summary>
        /// Verify the product form fields that are displayed
        /// </summary>
        private void VerifyProductFormDisplay()
        {
            MaterializeSelectGroup.VerifyMaterializeSelectField(QuotePom.DocketTypeSelectGroup, QuotePom.QuoteTestData.DocketType, FieldCss.Initial);

            MaterializeSelectGroup.VerifyMaterializeSelectField(QuotePom.DocketSizeSelectGroup, QuotePom.QuoteTestData.DocketSize, FieldCss.Initial);

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.DocketQuantityInputGroup, QuotePom.QuoteTestData.DocketQuantity, FieldCss.Initial);
        }

        private void VerifyValidation()
        {
            ///
            /// this needs to be repeated for each group 
            var input = MaterializeInputGroup.GetMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message);

            // Click in the field to make it active
            input.Click();

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Touched);

            // Enter a valid value
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo("This is a test message"));
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Valid);

            // Enter an invalid value
            input.Clear();
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo(string.Empty));

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Invalid);

            // Clear the fields
            QuotePom.Clear.Click();

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo(null));

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Invalid);

            // Enter a valid value
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo("This is a test message"));

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Valid);

            // Clear the fields
            QuotePom.Clear.Click();

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo(null));

            // Submit the form
            QuotePom.Submit.Click();

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Invalid);
        }
    }
}