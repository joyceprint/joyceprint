using JoycePrint.Web.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using JoycePrint.Web.Tests.Helpers.Materialize;

namespace JoycePrint.Web.Tests.TestData
{
    public class QuoteTestData
    {
        public string ClearText => "CLEAR";

        public string SubmitText => "SUBMIT";

        public string RecaptchaPublicKey => ConfigurationManager.AppSettings.Get("RecaptchaPublicKey");

        private MaterializeInputGroup _message;

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
                if (_message == null || !_message.Initialized)
                {
                    _message = new MaterializeInputGroup();

                    InitializeMessage(_message);
                }

                return _message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageTestData"></param>
        private void InitializeMessage(MaterializeInputGroup messageTestData)
        {
            messageTestData.IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial;
            messageTestData.IconText = "message";

            messageTestData.InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial;
            messageTestData.InputText = null;

            messageTestData.LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial;
            messageTestData.LabelText = "Message";

            messageTestData.ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial;
            messageTestData.ValidationLabelText = new Dictionary<string, string>
            {
                { "Required", "Please provide details" },
                { "Length", "Please reduce the number of characters" }
            };

            messageTestData.FieldInputType = "textarea";

            messageTestData.Initialized = true;
        }

        public IList<String> HelpCheckList
        {
            get
            {
                return new List<String>
                {
                    "Description of item",
                    "Full color / single color / spot color",
                    "Preferred stock",
                    "Finished size",
                    "Single-sided or double-sided",
                    "Number of pages",
                    "Quantity",
                    "Timing",
                    "Other information"
                };
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
                    IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial,
                    IconText = "business",

                    InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial,
                    LabelText = "Company",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial,
                    ValidationLabelText = new Dictionary<string, string>
                    {
                        { "Required", "Company name is a required field" },
                        { "RegEx", "Please check company name" },
                        { "Length", "Too many characters used" }
                    },

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
        public MaterializeInputGroup Name
        {
            get
            {
                var messageTestData = new MaterializeInputGroup
                {
                    IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial,
                    IconText = "perm_identity",

                    InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial,
                    LabelText = "Name",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial,
                    ValidationLabelText = new Dictionary<string, string>
                    {
                        { "Required", "Name is a required field" },
                        { "RegEx", "Please check your name" },
                        { "Length", "Please enter your name" }
                    },

                    FieldInputType = "input"
                };

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
                var messageTestData = new MaterializeInputGroup
                {
                    IconClasses = MaterializeCssStyles.MaterializeInputGroupIconClassesInitial,
                    IconText = "phone",

                    InputClasses = MaterializeCssStyles.MaterializeInputGroupInputClassesInitial,
                    InputText = null,

                    LabelClasses = MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial,
                    LabelText = "Phone",

                    ValidationLabelClasses = MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial,
                    ValidationLabelText = new Dictionary<string, string>
                    {
                        { "Required", "Phone is a required field" },
                        { "RegEx", "Please use a valid phone number" },
                        { "Length", "Please correct the contact number" }
                    },

                    FieldInputType = "input"
                };

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
                messageTestData.ValidationLabelText = new Dictionary<string, string>
                    {
                        { "Required", "Email is a required field" },
                        { "RegEx", "Please enter a valid email address" },
                        { "Length", "Did you forget your email" }
                    };

                messageTestData.FieldInputType = "input";

                return messageTestData;
            }
        }

        #endregion
    }
}