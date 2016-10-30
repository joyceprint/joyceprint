using JoycePrint.Domain.Business;

namespace JoycePrint.Domain.Models
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

        /// <summary>
        /// The additional information given by the client
        /// </summary>
        public string Message { get; set; }

        public QuoteRequest()
        {
            Contact = new Contact();
            DocketBook = new DocketBook();
        }

        public void SendEmail()
        {
            Email email = new Email();
            email.SendEmail(this);
        }
    }
}