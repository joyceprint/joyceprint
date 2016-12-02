using System.Web.Mvc;

using JoycePrint.Domain.Models;

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
            // TODO: ::BUG:: Enums not getting passed back
            //model.SendEmail();
            
            return View("Index", model);
        }
    }
}