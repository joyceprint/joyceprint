using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("")]
    public class SinglePageController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}