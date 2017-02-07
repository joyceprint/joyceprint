using System;
using System.Configuration;
using System.Runtime.InteropServices;

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
        public string Message { get; set; }
    }
}