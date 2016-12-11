using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("aboutus")]
    public class AboutUsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("AboutUs");
        }
    }
}