using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("contactus")]
    public class ContactUsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("ContactUs");
        }
    }
}