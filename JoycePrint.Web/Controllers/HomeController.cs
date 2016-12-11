using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}