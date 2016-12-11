namespace JoycePrint.Web.Tests.TestData
{
    public class QuoteTestData
    {
        public string BannerTopText => "Trade Docket Books";

        public string BannerBottomText => "Delivery To Ireland & The UK";

        public string ClearText => "CLEAR";

        public string SubmitText => "SUBMIT";

        public MaterializeTestData Message
        {
            get
            {
                var messageTestData = new MaterializeTestData();
                messageTestData.IconClasses = "material-icons prefix orange-text text-accent-4";
                messageTestData.IconText = "message";

                messageTestData.InputClasses = "materialize-textarea validate";
                messageTestData.InputText = null;

                messageTestData.LabelClasses = null;
                messageTestData.LabelText = "Message";

                messageTestData.ValidationLabelClasses = "val-msg";
                messageTestData.ValidationLabelText = "Any additional information";

                return messageTestData;
            }
        }
    }
}
