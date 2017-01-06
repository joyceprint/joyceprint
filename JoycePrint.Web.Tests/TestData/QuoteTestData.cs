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

        public string RecaptchaPublicKey => ConfigurationManager.AppSettings.Get("RecaptchaPublicKey");

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
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

                messageTestData.FieldInputType = "textarea";

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
                    HeaderCss = MaterializeCssStyles.MaterializeCollapseHeaderCssInitial,
                    BodyCss = MaterializeCssStyles.MaterializeCollapseBodyCssInitial,
                    HeaderTitleText = "Paper Size",
                    HeaderIconClasses = "material-icons",
                    HeaderIconText = "filter_drama",
                    InformationTitleText = "Available Sizes",
                    InformationImage = $"{Urls.UrlDellDev}/Content/images/jp-card-paper-size.jpg",
                    InformationBodyText = $"A4 - 210 x 297mm{Environment.NewLine}A5 - Half A4 - 148.5 x 210mm{Environment.NewLine}A6 - Quarter A4 - 105 x 148.5mm{Environment.NewLine}DL - Third A4 - 99 x 210mm",
                    CollapseWaitForId = "img-paper-size"
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
                    InformationBodyText = $"A6 - Shape: Protrait Size: 105 x 148.5mm{Environment.NewLine}A6 - Shape: Landscape Size: 148.5 x 105mm{Environment.NewLine}DL - Shape: Protrait Size: 99 x 210mm{Environment.NewLine}DL - Shape: Landscape Size: 210 x 99mm{Environment.NewLine}A5 - Shape: Protrait Size: 148.5 x 210mm{Environment.NewLine}A5 - Shape: Landscape Size: 210 x 148.5mm{Environment.NewLine}A4 - Shape: Protrait Size: 210 x 297mm{Environment.NewLine}A4 - Shape: Landscape Size: 297 x 210mm{Environment.NewLine}A3 - Shape: Landscape Size: 400 x 297mm",
                    CollapseWaitForId = "img-paper-orientation"
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
                    InformationBodyText = $"Duplicate (2 sheets in a Set - 100 sets per book){Environment.NewLine}Triplicate (3 sheets in a Set - 50 sets per book){Environment.NewLine}Quad (4 sheets in a Set - 50 sets per book)",
                    CollapseWaitForId = "img-book-type"
                });

                return helpTestData;
            }
        }

        #region Contact Form Input

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeInputGroup Company
        {
            get
            {
                return new MaterializeInputGroup
                {
                    IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesActive,
                    IconText = "business",

                    InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesActive,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesActive,
                    LabelText = "Company",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesActive,
                    ValidationLabelText = "Please enter the company name",

                    FieldInputType = "input"
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeInputGroup Position
        {
            get
            {
                var messageTestData = new MaterializeInputGroup();
                messageTestData.IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesOptional;
                messageTestData.IconText = "recent_actors";

                messageTestData.InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesOptional;
                messageTestData.InputText = null;

                messageTestData.LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesOptional;
                messageTestData.LabelText = "Position";

                messageTestData.ValidationLabelClasses = null;
                messageTestData.ValidationLabelText = null;

                messageTestData.FieldInputType = "input";

                return messageTestData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeInputGroup Name
        {
            get
            {
                var messageTestData = new MaterializeInputGroup();
                messageTestData.IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial;
                messageTestData.IconText = "perm_identity";

                messageTestData.InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial;
                messageTestData.InputText = null;

                messageTestData.LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial;
                messageTestData.LabelText = "Name";

                messageTestData.ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial;
                messageTestData.ValidationLabelText = "Please enter your name";

                messageTestData.FieldInputType = "input";

                return messageTestData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeInputGroup Phone
        {
            get
            {
                var messageTestData = new MaterializeInputGroup();
                messageTestData.IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial;
                messageTestData.IconText = "phone";

                messageTestData.InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial;
                messageTestData.InputText = null;

                messageTestData.LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial;
                messageTestData.LabelText = "Telephone Number";

                messageTestData.ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial;
                messageTestData.ValidationLabelText = "Please enter a contact number";

                messageTestData.FieldInputType = "input";

                return messageTestData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeInputGroup Email
        {
            get
            {
                var messageTestData = new MaterializeInputGroup();
                messageTestData.IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial;
                messageTestData.IconText = "email";

                messageTestData.InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial;
                messageTestData.InputText = null;

                messageTestData.LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial;
                messageTestData.LabelText = "Email";

                messageTestData.ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial;
                messageTestData.ValidationLabelText = "Did you forget your email";

                messageTestData.FieldInputType = "input";

                return messageTestData;
            }
        }

        #endregion

        #region Docket Form Input

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeSelectGroup DocketType
        {
            get
            {
                return new MaterializeSelectGroup
                {
                    IconClasses = MaterializeCssStyles.MaterializeSelectGroupIconClassesInitial,
                    IconText = "label",

                    InputClasses = MaterializeCssStyles.MaterializeSelectGroupInputClassesInitial,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeSelectGroupLabelClassesInitial,
                    LabelText = "Docket Type",

                    SpanClasses = MaterializeCssStyles.MaterializeSelectGroupSpanClassesInitial,
                    SpanText = "▼",

                    UnOrderedClasses = MaterializeCssStyles.MaterializeSelectGroupUnOrderedListClassesInitial,
                    UnOrderedSelectItemClasses = MaterializeCssStyles.MaterializeSelectGroupUnOrderedListSelectedItemClassesInitial,
                    UnOrderedSelectedItemText = "Type",

                    SelectListClasses = MaterializeCssStyles.MaterializeSelectGroupSelectListClassesInitial,                    
                    SelectListSelectedItemText = "Type",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeSelectGroupValidationLabelClassesInitial,
                    ValidationLabelText = "Please make a selection",

                    FieldInputType = "input"
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeSelectGroup DocketSize
        {
            get
            {
                return new MaterializeSelectGroup
                {
                    IconClasses = MaterializeCssStyles.MaterializeSelectGroupIconClassesInitial,
                    IconText = "label",

                    InputClasses = MaterializeCssStyles.MaterializeSelectGroupInputClassesInitial,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeSelectGroupLabelClassesInitial,
                    LabelText = "Docket Size",

                    SpanClasses = MaterializeCssStyles.MaterializeSelectGroupSpanClassesInitial,
                    SpanText = "▼",

                    UnOrderedClasses = MaterializeCssStyles.MaterializeSelectGroupUnOrderedListClassesInitial,
                    UnOrderedSelectItemClasses = MaterializeCssStyles.MaterializeSelectGroupUnOrderedListSelectedItemClassesInitial,
                    UnOrderedSelectedItemText = "Size",

                    SelectListClasses = MaterializeCssStyles.MaterializeSelectGroupSelectListClassesInitial,                    
                    SelectListSelectedItemText = "Size",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeSelectGroupValidationLabelClassesInitial,
                    ValidationLabelText = "Please make a selection",

                    FieldInputType = "input"
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Extension Method UpdateCssTo is used to change the css classes for this object
        /// If there are validation label(s) the required label text is the default
        /// </remarks>
        public MaterializeInputGroup DocketQuantity
        {
            get
            {
                return new MaterializeInputGroup
                {
                    IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial,
                    IconText = "label",

                    InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial,
                    LabelText = "Quantity",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial,
                    ValidationLabelText = "How many ?",

                    FieldInputType = "input"
                };
            }
        }

        #endregion
    }
}