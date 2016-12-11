using System.Web.Mvc;
using JoycePrint.Domain.Models;

namespace JoycePrint.Web.Controllers
{
    [Route("quote")]
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
            model.SendEmail();

            // Here I want to return a partial view that can be used as a modal              
            return View("Index");
        }
    }
}