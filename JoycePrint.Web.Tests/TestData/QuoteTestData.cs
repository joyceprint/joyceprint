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
            messageTestData.ValidationLabelText = "Please provide details";

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

        public string CompanyValidationMessage = "Company name is a required field";

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
                    ValidationLabelText = CompanyValidationMessage,

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
                    ValidationLabelText = "Please check your name",

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
                    ValidationLabelText = "Please use a valid phone number",

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
                messageTestData.ValidationLabelText = "Please enter a valid email address";

                messageTestData.FieldInputType = "input";

                return messageTestData;
            }
        }

        #endregion
    }
}