using System.Web.Mvc;
using JoycePrint.Domain.Enums;
using JoycePrint.Domain.Models;

namespace JoycePrint.Web.Controllers
{
    [Route("quote")]
    public class QuoteController : BaseController
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
            var notificationType = model.SendEmail() ? NotificationType.SUCCESS : NotificationType.FAILURE;

            TempData["NotificationType"] = notificationType;
            
            return RedirectToAction("Index", "Notification");
        }
    }
}