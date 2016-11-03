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
    }
}