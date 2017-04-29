using System.Web.Mvc;
using JoycePrint.Domain.Enums;
using JoycePrint.Domain.Models;

namespace JoycePrint.Web.Controllers
{
    [Route("quote")]
    public class QuoteController : BaseController
    {
        /// <summary>
        /// This method should only be called from the Single Page Index view, using ChildActionOnly stops this view from being called by the url
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Index()
        {
            var model = new QuoteRequest();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(QuoteRequest model)
        {
            if (ModelState.IsValid)
            {
                var notificationType = model.SendEmail() ? NotificationType.Success : NotificationType.Failure;

                TempData["NotificationType"] = notificationType;

                return RedirectToAction("Index", "Notification");
            }

            var data = RenderViewToString("Index", ViewData, ControllerContext, model, "Quote", "Index");
            
            return Json(new { view = data, target = "quote" }, JsonRequestBehavior.AllowGet);            
        }
    }
}