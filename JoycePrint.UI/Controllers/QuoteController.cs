using JoycePrint.Models;
using System.Web.Mvc;

namespace JoycePrint.UI.Controllers
{
    [Route("/")]
    public class QuoteController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
            var model = new QuoteRequest();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(QuoteRequest model)
        {

            return View("Index");
        }
    }
}