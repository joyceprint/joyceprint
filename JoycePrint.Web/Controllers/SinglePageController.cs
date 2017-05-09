using System.Web.Mvc;
using JoycePrint.Web.Attributes;

namespace JoycePrint.Web.Controllers
{
    [Route("")]
    public class SinglePageController : BaseController
    {
        [HttpGet]
        [PageAnalysis(Name = "/", Title = "Trade Docket Books")]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}