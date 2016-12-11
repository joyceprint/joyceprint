using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("services")]
    public class ServicesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}