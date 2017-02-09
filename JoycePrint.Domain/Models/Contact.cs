using System;

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
        [Obsolete("This property is currently not in use")]
        public string Position { get; set; }

        /// <summary>
        /// The name of the contact
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The phone number of the contact
        /// </summary>        
        public string Phone { get; set; }
        
        /// <summary>
        /// The contacts email address
        /// </summary>        
        public string Email { get; set; }
    }
}