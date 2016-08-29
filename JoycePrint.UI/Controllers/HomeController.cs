using System.Web.Mvc;

namespace JoycePrint.UI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("contact")]
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View("ContactUs");
        }

        [Route("about")]
        [HttpGet]
        public ActionResult AboutUs()
        {
            return View("AboutUs");
        }
    }
}