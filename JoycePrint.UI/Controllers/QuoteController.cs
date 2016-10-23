using System.Web.Mvc;

namespace JoycePrint.UI.Controllers
{
    [Route("/")]
    public class QuoteController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        //[HttpPost]
        //public ActionResult Index()
        //{
        //    return View("Index");
        //}
    }
}