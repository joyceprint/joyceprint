namespace JoycePrint.Models
{
    public class QuoteRequest
    {
        /// <summary>
        /// The client contact information that requested the quote
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// The docket book information for the quote
        /// </summary>
        public DocketBook DocketBook { get; set; }

        public QuoteRequest()
        {
            Contact = new Contact();
            DocketBook = new DocketBook();
        }
    }
}