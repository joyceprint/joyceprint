using System.Linq;
using System.Web.Mvc;

// ReSharper disable once CheckNamespace
namespace JoycePrint.Web
{
    public class ViewEngine : RazorViewEngine
    {
        public ViewEngine()
        {
            var newLocationFormat = new[]
            {
                "~/Views/{1}/Partial/{0}.cshtml",
                "~/Views/Shared/Partial/{0}.cshtml",
                "~/Views/{0}.cshtml"                
            };

            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}