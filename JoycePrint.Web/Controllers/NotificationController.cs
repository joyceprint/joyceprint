using System.Web.Mvc;
using JoycePrint.Domain.Enums;
using JoycePrint.Domain.Models;

namespace JoycePrint.Web.Controllers
{
    [RoutePrefix("notification")]
    public class NotificationController : BaseController
    {
        /// <summary>
        /// This displays the notification to the user with the result of the email send operation                
        /// </summary>
        /// <returns></returns>           
        [HttpGet]
        [ChildActionOnly]
        [Route("")]
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