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
            //model.SendEmail();

            // move all this to a notification controller - and make notification it's own component
            var notification = new Notification();

            notification.SetNotification(NotificationType.SUCCESS);

            var data = RenderPartialViewToString(ControllerContext, notification.ViewName, notification);
        
            return Content(data);
        }
    }
}