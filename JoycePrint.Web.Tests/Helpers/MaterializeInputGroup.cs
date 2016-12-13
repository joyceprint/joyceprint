using System.Diagnostics.CodeAnalysis;
using JoycePrint.Web.Tests.Enums;
using OpenQA.Selenium;

namespace JoycePrint.Web.Tests.Helpers
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class MaterializeInputGroup
    {
        #region Properties

        public string IconText { get; set; }

        public string IconClasses { get; set; }

        public string InputText { get; set; }

        public string InputClasses { get; set; }

        public string LabelText { get; set; }

        public string LabelClasses { get; set; }

        public string ValidationLabelText { get; set; }

        public string ValidationLabelClasses { get; set; }

        public string FieldInputType { get; set; }

        #endregion

        /// <summary>
        /// Verfiy the materialize fields state when the page is loaded       
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="testData">The test data to be used for the comparision</param>
        /// <param name="updateCssTo">The css style required, the field will have it's css updated to this type</param>
        public static void VerifyMaterializeInputField(IWebElement inputGroupContainer, MaterializeInputGroup testData, FieldCss updateCssTo)
        {
            IWebElement iconElement = null;
            IWebElement inputElement = null;
            IWebElement labelElement = null;
            IWebElement validationLabelElement = null;
            string fieldName = null;

            GetMaterializeWebElements(inputGroupContainer, ref iconElement, ref inputElement, ref labelElement, ref validationLabelElement, ref fieldName, testData.FieldInputType);

            testData.IconClasses.UpdateCssTo(updateCssTo).MatchesActualCss(iconElement.GetAttribute("class"), $"{fieldName} Icon Classes");
            testData.IconText.MatchesActual(iconElement.Text, $"{fieldName} Icon Text");

            if (testData.InputClasses != null)
                testData.InputClasses.UpdateCssTo(updateCssTo).MatchesActualCss(inputElement.GetAttribute("class"), $"{fieldName} Input Classes");

            if (testData.InputText != null)
                testData.InputText.MatchesActual(inputElement.Text, $"{fieldName} Input Text");

            if (testData.LabelClasses != null)
                testData.LabelClasses.UpdateCssTo(updateCssTo).MatchesActualCss(labelElement.GetAttribute("class"), $"{fieldName} Label Classes");

            if (testData.LabelText != null)
                testData.LabelText.MatchesActual(labelElement.Text, $"{fieldName} Label Text");

            // Return here is there's no validation label associated with the control
            if (null == validationLabelElement) return;

            testData.ValidationLabelClasses.UpdateCssTo(updateCssTo).MatchesActualCss(validationLabelElement.GetAttribute("class"), $"{fieldName} Validation Label Classes");

            // We only check the validation if it's displayed
            if (validationLabelElement.Displayed)
                testData.ValidationLabelText.MatchesActual(validationLabelElement.Text, $"{fieldName} Validation Label Text");
        }

        /// <summary>
        /// This method gets the input group fields for the inputGroupContainer passed in
        /// The input arguments are passed in as references so we don't have to use globals or multiple returns
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="iconElement">The icon element will be stored here</param>
        /// <param name="inputElement">The input element will be stored here</param>
        /// <param name="labelElement">The label element will be stored here</param>
        /// <param name="validationLabelElement">The validation label element will be stored here</param>
        /// <param name="fieldName">The name of the input field for the input group</param>
        /// <param name="inputTag">The input tag name to look for when setting the input element</param>
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        public static void GetMaterializeWebElements(IWebElement inputGroupContainer, ref IWebElement iconElement, ref IWebElement inputElement, ref IWebElement labelElement, ref IWebElement validationLabelElement, ref string fieldName, string inputTag)
        {
            iconElement = inputGroupContainer.FindElement(By.TagName("i"));
            inputElement = inputGroupContainer.FindElement(By.TagName(inputTag));

            var labelElements = inputGroupContainer.FindElements(By.TagName("label"));
            labelElement = labelElements[0];

            fieldName = labelElement.Text;

            if (labelElements.Count != 2) return;
            validationLabelElement = labelElements[1];
        }
    }
}