using System.Diagnostics.CodeAnalysis;
using JoycePrint.Web.Tests.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JoycePrint.Web.Tests.Helpers.Materialize
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class MaterializeCollapse : IMaterializeGroup
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

        public string CollapseWaitForId { get; set; }

        #endregion

        /// <summary>
        /// This method gets the collapse group fields for the headerContainer passed in
        /// The input arguments are passed in as references so we don't have to use globals or multiple returns
        /// </summary>
        /// <param name="headerContainer">The materialize header container that is used to get the elements that need to be checked</param>
        /// <param name="iconElement">The header icon element</param>
        /// <param name="headerText">The header element containing the header text</param>        
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        public static void GetHeaderElements(IWebElement headerContainer, ref IWebElement iconElement, ref IWebElement headerText)
        {
            iconElement = headerContainer.FindElement(By.TagName("i"));
            headerText = headerContainer.FindElement(By.TagName("span"));
        }

        /// <summary>
        /// This method gets the input group fields for the inputGroupContainer passed in
        /// The input arguments are passed in as references so we don't have to use globals or multiple returns
        /// </summary>
        /// <param name="bodyContainer">The materialize body container that is used to get the elements that need to be checked</param>
        /// <param name="imageElement">The image element will be stored here</param>
        /// <param name="bodyTitleText">The body title text element will be stored here</param>
        /// <param name="bodyText">The body text element will be stored here</param>        
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        public static void GetBodyElements(IWebElement bodyContainer, ref IWebElement imageElement, ref IWebElement bodyTitleText, ref IWebElement bodyText)
        {
            imageElement = bodyContainer.FindElement(By.TagName("img"));
            bodyTitleText = bodyContainer.FindElement(By.ClassName("card-title"));
            bodyText = bodyContainer.FindElement(By.TagName("ul"));
        }

        /// <summary>
        /// Verfiy the materialize fields state when the page is loaded       
        /// </summary>
        /// <param name="headerContainer">The materialize header container that is used to get the elements that need to be checked</param>
        /// <param name="bodyContainer">The materialize body container that is used to get the elements that need to be checked</param>
        /// <param name="testData">The test data to be used for the comparision</param>
        /// <param name="updateCssTo">The css style required, the field will have it's css updated to this type</param>
        /// <param name="wait">A wait used to wait for the collapse animation to complete</param>
        public static void VerifyMaterializeCollapse(IWebElement headerContainer, IWebElement bodyContainer, MaterializeCollapse testData, FieldCss updateCssTo, WebDriverWait wait)
        {
            IWebElement iconElement = null;
            IWebElement headerText = null;
            IWebElement imageElement = null;
            IWebElement bodyTitleText = null;
            IWebElement bodyText = null;

            GetHeaderElements(headerContainer, ref iconElement, ref headerText);
            GetBodyElements(bodyContainer, ref imageElement, ref bodyTitleText, ref bodyText);
            
            testData.UpdateCssForCollapse(testData.HeaderCss, updateCssTo).MatchesActual(headerContainer.GetAttribute("class"), "Header Classes");

            testData.HeaderIconClasses.MatchesActual(iconElement.GetAttribute("class"), "Header Icon Classes");
            testData.HeaderIconText.MatchesActual(iconElement.Text, "Header Icon Text");
            testData.HeaderTitleText.MatchesActual(headerText.Text, "Header Title Text");

            testData.BodyCss.MatchesActual(bodyContainer.GetAttribute("class"), "Body Css Classes");

            if (updateCssTo == FieldCss.Active)
            {
                // Wait for the animation to finish
                wait.Until(ExpectedConditions.TextToBePresentInElement(bodyTitleText, testData.InformationTitleText));

                testData.InformationTitleText.MatchesActual(bodyTitleText.Text, "Body Title Text");
                testData.InformationImage.MatchesActual(imageElement.GetAttribute("src"), "Body Image Src");
                testData.InformationBodyText.MatchesActual(bodyText.Text, "Body Text");
            }
            else if (updateCssTo == FieldCss.Initial)
            {
                // Wait for the animation to finish
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id(testData.CollapseWaitForId)));

                Assert.IsFalse(bodyTitleText.Displayed, $"The Body Title for {testData.InformationTitleText} should be hidden");
                Assert.IsFalse(imageElement.Displayed, $"The Body Image for {testData.InformationImage} should be hidden");
                Assert.IsFalse(bodyText.Displayed, $"The Body Text for {testData.InformationBodyText} should be hidden");
            }
        }
    }
}