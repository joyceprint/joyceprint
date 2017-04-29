using System.ComponentModel.DataAnnotations;

namespace JoycePrint.Domain.Models
{
    public class Enquiry
    {
        /// <summary>
        /// Is this enquiry a quote
        /// </summary>
        public bool IsQuote { get; set; }

        /// <summary>
        /// Is this enquiry a question
        /// </summary>       
        public bool IsQuestion { get; set; }

        /// <summary>
        /// The additional information given by the client
        /// </summary>
        /// <remarks>
        /// To stop user from overfilling the enquiry field and possibly breaking the code
        /// We may need to increase this value
        /// </remarks>
        [Required(ErrorMessage = "Please provide details")]
        [StringLength(1000, ErrorMessage = "Please reduce the number of characters")]
        public string Message { get; set; }
    }
}