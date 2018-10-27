using System.Reflection;
using System.Threading;
using JoycePrint.Web.Tests.Helpers;
using JoycePrint.Web.Tests.PageObjectModels;
using JoycePrint.Web.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.Helpers.Materialize;
using OpenQA.Selenium.Interactions;

namespace JoycePrint.Web.Tests.Tests
{
    /// <summary>
    /// Performs a simple login logout test and checks the users first, last and company name
    /// </summary>
    public class QuotePageTest : WebDriverTestBase
    {
        #region Base Properties & Functions

        private Actions _actions;

        protected override void RunTest(IWebDriver driver)
        {
            HeaderPom = new HeaderPom(driver);
            HeaderPom.NavQuote.Click();

            QuotePom = new QuotePom(driver);

            _actions = new Actions(driver);

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
            VerifyInputValidation(QuotePom.CompanyInputGroup, QuotePom.QuoteTestData.Company, "JoycePrint Ltd", new string('a', 151), "!");
            VerifyInputValidation(QuotePom.NameInputGroup, QuotePom.QuoteTestData.Name, "John Smith", new string('a', 151), "!");
            VerifyInputValidation(QuotePom.PhoneInputGroup, QuotePom.QuoteTestData.Phone, "0876481033", new string('2', 31), "!dsa");
            VerifyInputValidation(QuotePom.EmailInputGroup, QuotePom.QuoteTestData.Email, "test@email.com", string.Concat(new string('a', 255), "a@a.a"), "@#$!");

            VerifyTextareaValidation();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The input needs to have it's clicked function called to make the validation fire
        /// </remarks>
        private void VerifyInputValidation(IWebElement inputGroup, MaterializeInputGroup testData,
            string validValue, string invalidValueLength, string invalidValueRegEx)
        {
            var icon = MaterializeInputGroup.GetMaterializeIconField(inputGroup);
            var input = MaterializeInputGroup.GetMaterializeInputField(inputGroup, testData);

            // Clear the fields to reset everything
            QuotePom.Clear.Click();

            // Test - Initial
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Initial);

            // Test - Valid Value
            input.SendKeys(testData.UpdateTextTo(validValue));
            icon.Click();
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Valid);

            // Test - Invalid Regex Value
            input.Clear();
            input.SendKeys(testData.UpdateTextTo(invalidValueRegEx));
            icon.Click();
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Invalid, "RegEx");

            // Clear the fields
            input.SendKeys(testData.UpdateTextTo(string.Empty));
            QuotePom.Clear.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Initial);

            // Enter an invalid value - Length
            input.SendKeys(testData.UpdateTextTo(invalidValueLength));
            icon.Click();
            _actions.MoveToElement(input).Click().Build().Perform();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Invalid, "Length");

            // Enter an invalid value - Missing
            input.SendKeys(testData.UpdateTextTo(string.Empty));
            input.Clear();
            icon.Click();
            _actions.MoveToElement(input).Click().Build().Perform();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Invalid, "Required");

            // Enter a valid value
            input.Clear();
            input.SendKeys(testData.UpdateTextTo(validValue));
            icon.Click();
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Valid);

            // Clear out the test value
            testData.InputText = null;

            // Clear the fields
            QuotePom.Clear.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Initial);

            // Enter an invalid value
            input.SendKeys(testData.UpdateTextTo(invalidValueRegEx));

            // Submit the form and check the validation display
            QuotePom.Submit.Click();

            // Check in validation message
            input.Click();
            MaterializeInputGroup.VerifyMaterializeInputField(inputGroup, testData, FieldCss.Invalid, "RegEx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The input needs to have it's clicked function called to make the validation fire
        /// </remarks>
        private void VerifyTextareaValidation()
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