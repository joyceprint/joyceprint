using System.Web.Mvc;
using JoycePrint.Domain.Enums;
using JoycePrint.Domain.Models;

namespace JoycePrint.Web.Controllers
{
    [Route("notification")]
    public class NotificationController : BaseController
    {
        [HttpGet]
        public JsonResult Index()
        {
            var notification = new Notification();

            var notificationType = (NotificationType)TempData["NotificationType"];

            notification.SetNotification(notificationType);

            var data = RenderPartialViewToString(ControllerContext, notification.ViewName, notification);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }
    }
}