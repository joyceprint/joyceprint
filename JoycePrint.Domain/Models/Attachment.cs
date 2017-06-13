using System.Collections.Generic;
using System.Web;

namespace JoycePrint.Domain.Models
{
    public class Attachment
    {
        /// <summary>
        /// The attachment collection to attach to the email
        /// </summary>        
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }
    }
}