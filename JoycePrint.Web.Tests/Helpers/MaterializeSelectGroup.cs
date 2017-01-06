using System.Diagnostics.CodeAnalysis;
using JoycePrint.Web.Tests.Enums;
using OpenQA.Selenium;
using System.Web.UI.HtmlControls;

namespace JoycePrint.Web.Tests.Helpers
{
    public class MaterializeSelectGroup : IMaterializeGroup
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

        // TODO: Possibly make this an enum
        public string FieldInputType { get; set; }
        
        public string SpanText { get; set; }        

        public string SpanClasses { get; set; }

        public string UnOrderedClasses { get; set; }

        public string UnOrderedSelectItemClasses { get; set; }

        public string UnOrderedSelectedItemText { get; set; }

        public string SelectListClasses { get; set; }
        
        public string SelectListSelectedItemText { get; set; }

        #endregion

        /// <summary>
        /// Verfiy the materialize fields state when the page is loaded       
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="testData">The test data to be used for the comparision</param>
        /// <param name="updateCssTo">The css style required, the field will have it's css updated to this type</param>
        public static void VerifyMaterializeSelectField(IWebElement inputGroupContainer, MaterializeSelectGroup testData, FieldCss updateCssTo)
        {
            IWebElement iconElement = null;
            IWebElement inputElement = null;            
            IWebElement labelElement = null;
            IWebElement validationLabelElement = null;

            IWebElement spanElement = null;

            IWebElement ulElement = null;
            IWebElement liElement = null;

            IWebElement selectElement = null;
            IWebElement optionElement = null;

            string fieldName = null;

            GetMaterializeWebElements(inputGroupContainer, ref iconElement, ref inputElement, ref labelElement, ref validationLabelElement, ref spanElement, ref ulElement, ref selectElement, ref fieldName, testData.FieldInputType);

            liElement = updateCssTo.HasFlag(FieldCss.Initial) ? ulElement.FindElement(By.CssSelector("li:first-child")) : ulElement.FindElement(By.CssSelector("li[actve]"));
            optionElement = selectElement.FindElement(By.CssSelector("option[selected='selected']"));

            testData.UpdateCssTo(testData.IconClasses, updateCssTo).MatchesActualCss(iconElement.GetAttribute("class"), $"{fieldName} Icon Classes");
            testData.IconText.MatchesActual(iconElement.Text, $"{fieldName} Icon Text");

            if (testData.InputClasses != null)
                testData.UpdateCssTo(testData.InputClasses, updateCssTo).MatchesActualCss(inputElement.GetAttribute("class"), $"{fieldName} Input Classes");

            if (testData.InputText != null)
                testData.InputText.MatchesActual(inputElement.Text, $"{fieldName} Input Text");

            if (testData.LabelClasses != null)
                testData.UpdateCssTo(testData.LabelClasses, updateCssTo).MatchesActualCss(labelElement.GetAttribute("class"), $"{fieldName} Label Classes");

            if (testData.LabelText != null)
                testData.LabelText.MatchesActual(labelElement.Text, $"{fieldName} Label Text");

            if (testData.SpanClasses != null)
                testData.UpdateCssTo(testData.SpanClasses, updateCssTo).MatchesActualCss(spanElement.GetAttribute("class"), $"{fieldName} Span Classes");

            if (testData.SpanText != null)
                testData.SpanText.MatchesActual(spanElement.Text, $"{fieldName} Span Text");

            if (testData.UnOrderedClasses != null)
                testData.UpdateCssTo(testData.UnOrderedClasses, updateCssTo).MatchesActualCss(ulElement.GetAttribute("class"), $"{fieldName} UnOrderList Classes");

            if (testData.UnOrderedSelectItemClasses != null)
                testData.UpdateCssTo(testData.UnOrderedSelectItemClasses, updateCssTo).MatchesActualCss(liElement.GetAttribute("class"), $"{fieldName} UnOrderList Selected Item Classes");

            if (testData.UnOrderedSelectedItemText != null)
                testData.UnOrderedSelectedItemText.MatchesActual(liElement.GetAttribute("innerText"), $"{fieldName} UnOrderList Selected Item Text");            

            if (testData.SelectListClasses != null)
                testData.UpdateCssTo(testData.SelectListClasses, updateCssTo).MatchesActualCss(selectElement.GetAttribute("class"), $"{fieldName} SelectList Classes");
            
            if (testData.SelectListSelectedItemText != null)
                testData.SelectListSelectedItemText.MatchesActual(optionElement.GetAttribute("innerText"), $"{fieldName} SelectList Selected Item Text");

            // Return here is there's no validation label associated with the control
            if (null == validationLabelElement) return;

            testData.UpdateCssTo(testData.ValidationLabelClasses, updateCssTo).MatchesActualCss(validationLabelElement.GetAttribute("class"), $"{fieldName} Validation Label Classes");

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
        public static void GetMaterializeWebElements(IWebElement inputGroupContainer, ref IWebElement iconElement, ref IWebElement inputElement, ref IWebElement labelElement, ref IWebElement validationLabelElement, ref IWebElement spanElement, ref IWebElement ulElement, ref IWebElement selectElement, ref string fieldName, string inputTag)
        {
            iconElement = inputGroupContainer.FindElement(By.TagName("i"));
            inputElement = inputGroupContainer.FindElement(By.TagName(inputTag));

            spanElement = inputGroupContainer.FindElement(By.TagName("span"));
            ulElement = inputGroupContainer.FindElement(By.TagName("ul"));
            selectElement = inputGroupContainer.FindElement(By.TagName("select"));

            var labelElements = inputGroupContainer.FindElements(By.TagName("label"));
            labelElement = labelElements[0];

            fieldName = labelElement.Text;

            if (labelElements.Count != 2) return;
            validationLabelElement = labelElements[1];
        }
    }
}