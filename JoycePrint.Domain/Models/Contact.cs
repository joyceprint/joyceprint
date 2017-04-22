using System.ComponentModel.DataAnnotations;

namespace JoycePrint.Domain.Models
{
    public class Contact
    {
        /// <summary>
        /// The company the contact represents
        /// </summary>  
        [Required(ErrorMessage = "Company name is a required field")]      
        [MaxLength(150, ErrorMessage = "Too many characters used")]
        [RegularExpression(@"^( *)(([A-Za-z0-9]+)( *))*$", ErrorMessage = "Please check company name")]
        public string Company { get; set; }

        /// <summary>
        /// The name of the contact
        /// </summary>
        //[StringLength(150, MinimumLength = 1, ErrorMessage = "Please enter your name.")]
        //[RegularExpression(@"( *)(([A-Za-z]+)(-| *))*", ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        /// <summary>
        /// The contacts email address
        /// </summary>     
        //[EmailAddress(ErrorMessage = "Please enter a valid email address")]   
        //[StringLength(254, MinimumLength = 1, ErrorMessage = "Did you forget your email")]
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the contact
        /// </summary>  
        //[StringLength(150, MinimumLength = 1, ErrorMessage = "Please enter a contact number")]
        //[RegularExpression(@"( *)([0-9]+[ ]*[-]?[ ]*)*", ErrorMessage = "Please use a valid phone number")]
        public string Phone { get; set; }
    }
}