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

        public MaterializeInputGroup Message
        {
            get//moe the strings to a static class
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
    }
}
