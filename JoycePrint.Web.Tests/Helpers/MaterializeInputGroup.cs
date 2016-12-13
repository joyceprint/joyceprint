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

        #endregion

        /// <summary>
        /// Verfiy the materialize fields state when the page is loaded       
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="testData">The test data to be used for the comparision</param>
        /// <param name="updateCssTo">The css style required, the field will have it's css updated to this type</param>
        public static void VerifyMaterializeField(IWebElement inputGroupContainer, MaterializeInputGroup testData, FieldCss updateCssTo)
        {
            IWebElement iconElement = null;
            IWebElement inputElement = null;
            IWebElement labelElement = null;
            IWebElement validationLabelElement = null;
            string fieldName = null;

            GetMaterializeWebElements(inputGroupContainer, ref iconElement, ref inputElement, ref labelElement, ref validationLabelElement, ref fieldName, "textarea");

            AssertHelper.AssertAreEqual(testData.IconClasses.UpdateCssTo(updateCssTo), iconElement.GetAttribute("class"), $"{fieldName} Icon Classes");
            AssertHelper.AssertAreEqual(testData.IconText, iconElement.Text, $"{fieldName} Icon Text");

            AssertHelper.AssertAreEqual(testData.InputClasses.UpdateCssTo(updateCssTo), inputElement.GetAttribute("class"), $"{fieldName} Input Classes");

            if (testData.InputText != null)
                AssertHelper.AssertAreEqual(testData.InputText, inputElement.Text, $"{fieldName} Input Text");

            if (testData.LabelClasses != null)
                AssertHelper.AssertAreEqual(testData.LabelClasses.UpdateCssTo(updateCssTo), labelElement.GetAttribute("class"), $"{fieldName} Label Classes");

            if (testData.LabelText != null)
                AssertHelper.AssertAreEqual(testData.LabelText, labelElement.Text, $"{fieldName} Label Text");

            if (null == validationLabelElement) return;

            AssertHelper.AssertAreEqual(testData.ValidationLabelClasses.UpdateCssTo(updateCssTo), validationLabelElement.GetAttribute("class"), $"{fieldName} Validation Label Classes");

            // We only check the validation if it's displayed
            if (validationLabelElement.Displayed)
                AssertHelper.AssertAreEqual(testData.ValidationLabelText, validationLabelElement.Text, $"{fieldName} Validation Label Text");
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