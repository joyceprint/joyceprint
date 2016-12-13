using JoycePrint.Web.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace JoycePrint.Web.Tests.TestData
{
    public class QuoteTestData
    {
        public string BannerTopText => "Trade Docket Books";

        public string BannerBottomText => "Delivery To Ireland & The UK";

        public string ClearText => "CLEAR";

        public string SubmitText => "SUBMIT";

        public string RecaptchaSiteKey => ConfigurationManager.AppSettings.Get("RecaptchaSiteKey");

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// </remarks>
        public MaterializeInputGroup Message
        {
            get
            {
                var messageTestData = new MaterializeInputGroup();
                messageTestData.IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial;
                messageTestData.IconText = "message";

                messageTestData.InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial;
                messageTestData.InputText = null;

                messageTestData.LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial;
                messageTestData.LabelText = "Message";

                messageTestData.ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial;
                messageTestData.ValidationLabelText = "Any additional information";

                return messageTestData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// </remarks>
        public IList<MaterializeCollapse> Help
        {
            get
            {
                var helpTestData = new List<MaterializeCollapse>();

                helpTestData.Add(new MaterializeCollapse
                {
                    HeaderCss = MaterializeCssStyles.MaterializeCollapseHeaderCssActive,
                    BodyCss = MaterializeCssStyles.MaterializeCollapseBodyCssActive,
                    HeaderTitleText = "Paper Size",
                    HeaderIconClasses = "material-icons",
                    HeaderIconText = "filter_drama",
                    InformationTitleText = "Available Sizes",
                    InformationImage = $"{Urls.UrlDellDev}/Content/images/jp-card-paper-size.jpg",
                    InformationBodyText = $"A4 - 210 x 297mm{Environment.NewLine}A5 - Half A4 - 148.5 x 210mm{Environment.NewLine}A6 - Quarter A4 - 105 x 148.5mm{Environment.NewLine}DL - Third A4 - 99 x 210mm"
                });

                helpTestData.Add(new MaterializeCollapse
                {
                    HeaderCss = MaterializeCssStyles.MaterializeCollapseHeaderCssInitial,
                    BodyCss = MaterializeCssStyles.MaterializeCollapseBodyCssInitial,
                    HeaderTitleText = "Paper Orientation",
                    HeaderIconClasses = "material-icons",
                    HeaderIconText = "filter_drama",
                    InformationTitleText = "Paper Orientation",
                    InformationImage = $"{Urls.UrlDellDev}/Content/images/jp-card-paper-orientation.jpg",
                    InformationBodyText = $"A6 - Shape: Protrait Size: 105 x 148.5mm{Environment.NewLine}A6 - Shape: Landscape Size: 148.5 x 105mm{Environment.NewLine}DL - Shape: Protrait Size: 99 x 210mm{Environment.NewLine}DL - Shape: Landscape Size: 210 x 99mm{Environment.NewLine}A5 - Shape: Protrait Size: 148.5 x 210mm{Environment.NewLine}A5 - Shape: Landscape Size: 210 x 148.5mm{Environment.NewLine}A4 - Shape: Protrait Size: 210 x 297mm{Environment.NewLine}A4 - Shape: Landscape Size: 297 x 210mm{Environment.NewLine}A3 - Shape: Landscape Size: 400 x 297mm"
                });

                helpTestData.Add(new MaterializeCollapse
                {
                    HeaderCss = MaterializeCssStyles.MaterializeCollapseHeaderCssInitial,
                    BodyCss = MaterializeCssStyles.MaterializeCollapseBodyCssInitial,
                    HeaderTitleText = "Book Type",
                    HeaderIconClasses = "material-icons",
                    HeaderIconText = "filter_drama",
                    InformationTitleText = "Book Type",
                    InformationImage = $"{Urls.UrlDellDev}/Content/images/jp-card-book-type.jpg",
                    InformationBodyText = $"Duplicate (2 sheets in a Set - 100 sets per book){Environment.NewLine}Triplicate (3 sheets in a Set - 50 sets per book){Environment.NewLine}Quad (4 sheets in a Set - 50 sets per book)"
                });

                return helpTestData;
            }
        }
    }
}