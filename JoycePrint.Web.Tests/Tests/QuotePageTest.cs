using System.Reflection;
using System.Threading;
using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;
using JoycePrint.Web.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.Helpers.Materialize;

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

            VerifyValidation();
        }

        #endregion

        /// <summary>
        /// Verify the display of the page, all display checks will be called from here
        /// </summary>
        private void VerifyDisplay()
        {
            VerifyHelpDisplay();

            VerifyFormDisplay();

            VerifyMapDisplay();
        }

        /// <summary>
        /// Verify the map is displayed
        /// </summary>
        private void VerifyMapDisplay()
        {
            // TODO: Write this test
        }

        /// <summary>
        /// Verify the help that's displayed on the form
        /// </summary>
        private void VerifyHelpDisplay()
        {
            if (QuotePom.HelpChecklist.Count != QuotePom.QuoteTestData.HelpCheckList.Count)
                Assert.Fail($"The expected number of checklist items [{QuotePom.HelpChecklist.Count}] does not match the actual number of check list items [{QuotePom.QuoteTestData.HelpCheckList.Count}]");

            var index = 0;
            foreach (var item in QuotePom.QuoteTestData.HelpCheckList)
            {
                item.MatchesActual(QuotePom.HelpChecklist[index].Text, $"Checklist Item [{index}]");
                index++;
            }
        }

        /// <summary>
        /// Verify the form fields that are displayed
        /// </summary>
        private void VerifyFormDisplay()
        {
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.NameInputGroup, QuotePom.QuoteTestData.Name, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.PhoneInputGroup, QuotePom.QuoteTestData.Phone, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.EmailInputGroup, QuotePom.QuoteTestData.Email, FieldCss.Initial);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);

            QuotePom.QuoteTestData.ClearText.MatchesActual(QuotePom.Clear.Text, "Clear Button");
            QuotePom.QuoteTestData.SubmitText.MatchesActual(QuotePom.Submit.Text, "Submit Button");

            // The recaptcha functionality was removed for now - July 2018
            //Assert.IsTrue(QuotePom.Recaptcha.Displayed, "Recaptcha Missing");
            //QuotePom.Recaptcha.GetAttribute("data-sitekey").MatchesActual(QuotePom.QuoteTestData.RecaptchaPublicKey, "Recaptcha Public Key");

            // Verify the message field
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);
        }

        private void VerifyValidation()
        {
            VerifyCompanyValidation();
            VerifyMessageValidation();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The input needs to have it's clicked function called to make the validation fire
        /// </remarks>
        private void VerifyCompanyValidation()
        {
            var validationLabelText = "Company name is a required field";
            var validationLabelTextAlt = "Please check company name";

            // Clear the fields to reset everything
            QuotePom.Clear.Click();

            var input = MaterializeInputGroup.GetMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company);

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Initial);

            // Enter a valid value
            input.SendKeys(QuotePom.QuoteTestData.Company.UpdateTextTo("JoycePrint Ltd"));
            QuotePom.Form.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Valid);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Company.UpdateTextTo("!"));
            QuotePom.QuoteTestData.CompanyValidationMessage = validationLabelTextAlt;
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Invalid);

            // Clear the fields
            QuotePom.Clear.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Initial);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Company.UpdateTextTo("!"));
            QuotePom.Form.Click();
            QuotePom.QuoteTestData.CompanyValidationMessage = validationLabelTextAlt;
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Invalid);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Company.UpdateTextTo(string.Empty));
            input.Clear();
            input.SendKeys(Keys.Delete);
            QuotePom.QuoteTestData.CompanyValidationMessage = validationLabelText;
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Invalid);

            // Enter a valid value
            input.Clear();
            input.SendKeys(QuotePom.QuoteTestData.Company.UpdateTextTo("JoycePrint Ltd"));
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Valid);

            // Clear out the test value
            QuotePom.QuoteTestData.Company.InputText = null;

            // Clear the fields
            QuotePom.Clear.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Initial);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Company.UpdateTextTo("!"));

            // Submit the form and check the validation display
            QuotePom.Submit.Click();

            // Check in validation message
            QuotePom.QuoteTestData.CompanyValidationMessage = validationLabelTextAlt;
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, FieldCss.Invalid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The input needs to have it's clicked function called to make the validation fire
        /// </remarks>
        private void VerifyMessageValidation()
        {
            // Clear the fields to reset everything
            QuotePom.Clear.Click();

            var input = MaterializeInputGroup.GetMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message);

            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);

            // Enter a valid value
            input.Click();
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo("This is a test message"));
            QuotePom.Form.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Valid);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo(string.Empty));
            input.Clear();
            input.SendKeys(Keys.Delete);
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Invalid);

            // Clear the fields
            QuotePom.Clear.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);

            // Clear out the test value
            QuotePom.QuoteTestData.Message.InputText = null;

            // Clear the fields
            QuotePom.Clear.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Initial);

            // Enter an invalid value
            input.SendKeys(QuotePom.QuoteTestData.Message.UpdateTextTo(string.Empty));

            // Submit the form and check the validation display
            QuotePom.Submit.Click();

            // Because we're not active here we don't want to compare the validation label text
            MaterializeInputGroup.VerifyMaterializeInputField(QuotePom.MessageInputGroup, QuotePom.QuoteTestData.Message, FieldCss.Invalid);
        }
    }
}