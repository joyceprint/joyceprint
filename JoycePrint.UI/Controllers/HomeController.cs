using System.Web.Mvc;

namespace JoycePrint.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}