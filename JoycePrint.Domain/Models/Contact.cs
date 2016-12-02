using System.ComponentModel.DataAnnotations;

namespace JoycePrint.Domain.Models
{
    public class Contact
    {
        /// <summary>
        /// The company the contact represents
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The position held by the contact at the company
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// The name of the contact
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The phone number of the contact
        /// </summary>
        //[Phone(ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        // TODO: need to add to or over ride the .net methods
        /// <summary>
        /// The contacts email address
        /// </summary>
        //[EmailAddress(ErrorMessage = "Please enter a valid email address")]
        //[MaxLength(length: 254, ErrorMessage = "An email address cannot be more that 254 characters in length")]
        public string Email { get; set; }
    }
}