using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JoycePrint.Web.Tests.TestData
{
    public class MaterializeCollapse
    {
        #region Properties

        public string HeaderCss { get; set; }

        public string BodyCss { get; set; }

        public string HeaderIconText { get; set; }

        public string HeaderIconClasses { get; set; }

        public string HeaderTitleText { get; set; }

        public string InformationImage { get; set; }

        public string InformationTitleText { get; set; }

        public string InformationBodyText { get; set; }

        #endregion        

        /// <summary>
        /// This method gets the input group fields for the inputGroupContainer passed in
        /// The input arguments are passed in as references so we don't have to use globals or multiple returns
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="iconElement">The icon element will be stored here</param>
        /// <param name="inputElement">The input element will be stored here</param>
        /// <param name="labelElement">The label element will be stored here</param>
        /// <param name="validationLabelElement">The validation label element will be stored here</param>
        /// <param name="inputTag">The input tag name to look for when setting the input element</param>
        public static void GetHeaderElements(IWebElement headerContainer, ref IWebElement iconElement, ref IWebElement headerText)
        {
            iconElement = headerContainer.FindElement(By.TagName("i"));
            headerText = headerContainer.FindElement(By.TagName("span"));
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
        /// <param name="inputTag">The input tag name to look for when setting the input element</param>
        public static void GetBodyElements(IWebElement bodyContainer, ref IWebElement imageElement, ref IWebElement bodyTitleText, ref IWebElement bodyText)
        {
            imageElement = bodyContainer.FindElement(By.TagName("img"));
            bodyTitleText = bodyContainer.FindElement(By.ClassName("card-title"));
            bodyText = bodyContainer.FindElement(By.TagName("ul"));
        }

        /// <summary>
        /// Verfiy the materialize fields state when the page is loaded       
        /// </summary>
        /// <param name="inputGroupContainer">The materialize input group container that is used to get the elements that need to be checked</param>
        /// <param name="testData">The test data to be used for the comparision</param>
        public static void VerifyMaterializeCollapse(IWebElement headerContainer, IWebElement bodyContainer, MaterializeCollapse testData, FieldCss updateCssTo, WebDriverWait wait)
        {
            IWebElement iconElement = null;
            IWebElement headerText = null;
            IWebElement imageElement = null;
            IWebElement bodyTitleText = null;
            IWebElement bodyText = null;

            GetHeaderElements(headerContainer, ref iconElement, ref headerText);
            GetBodyElements(bodyContainer, ref imageElement, ref bodyTitleText, ref bodyText);
            testData.HeaderCss.UpdateCssTo(updateCssTo).MatchesActual(headerContainer.GetAttribute("class").ToString(), "Header Classes");

            testData.HeaderIconClasses.MatchesActual(iconElement.GetAttribute("class").ToString(), "Header Icon Classes");
            testData.HeaderIconText.MatchesActual(iconElement.Text, "Header Icon Text");
            testData.HeaderTitleText.MatchesActual(headerText.Text, "Header Title Text");

            testData.BodyCss.MatchesActual(bodyContainer.GetAttribute("class").ToString(), "Body Css Classes");

            if (updateCssTo == FieldCss.Active)
            {
                wait.Until(ExpectedConditions.TextToBePresentInElement(bodyTitleText, testData.InformationTitleText));

                testData.InformationTitleText.MatchesActual(bodyTitleText.Text, "Body Title Text");
                testData.InformationImage.MatchesActual(imageElement.GetAttribute("src"), "Body Image Src");
                testData.InformationBodyText.MatchesActual(bodyText.Text, "Body Text");
            }
            else if (updateCssTo == FieldCss.Initial)
            {
                Assert.IsFalse(bodyTitleText.Displayed, $"The Body Title for {testData.InformationTitleText} should be hidden");
                Assert.IsFalse(imageElement.Displayed, $"The Body Image for {testData.InformationImage} should be hidden");
                Assert.IsFalse(bodyText.Displayed, $"The Body Text for {testData.InformationBodyText} should be hidden");
            }
        }
    }
}