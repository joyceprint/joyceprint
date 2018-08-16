using System.Linq;
using System.Web.Mvc;

namespace DocketBooks.Web
{
    public class ViewEngine : RazorViewEngine
    {
        public ViewEngine()
        {
            var newLocationFormat = new[]
            {
                "~/Views/{1}/Partial/{0}.cshtml",
                "~/Views/Shared/Partial/{0}.cshtml",
                "~/Views/Shared/{1}/{0}.cshtml",
                "~/Views/{0}.cshtml"                
            };

            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}