using System.Web.Mvc;

namespace JoycePrint.UI.Controllers
{
    [Route("/")]
    public class ContactUsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("ContactUs");
        }
    }
}