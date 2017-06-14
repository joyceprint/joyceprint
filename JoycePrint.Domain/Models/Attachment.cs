using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Upload)]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public Attachment()
        {
            Files = new List<HttpPostedFileBase>();
        }    
    }
}