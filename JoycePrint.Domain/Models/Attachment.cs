using System.Collections.Generic;
using System.Web;
using JoycePrint.Domain.Attributes;

namespace JoycePrint.Domain.Models
{
    public class Attachment
    {
        /// <summary>
        /// The attachment collection to attach to the email
        /// </summary>        
        /// <remarks>
        /// The HttpPostedFileBase is created to substitute HttpPostedFile in MVC applications for better unit testing.
        /// </remarks>
        //[FileSize(10240)]
        //[FileTypes("jpg,jpeg,png")]
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }

        public Attachment()
        {
            Attachments = new List<HttpPostedFileBase>();
        }    
    }
}