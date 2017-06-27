using System.Web.Mvc;
using JoycePrint.Domain.Enums;
using JoycePrint.Domain.Mail;
using JoycePrint.Domain.Models;
using JoycePrint.Web.Attributes;

namespace JoycePrint.Web.Controllers
{
    [RoutePrefix("quote")]
    public class QuoteController : BaseController
    {
        /// <summary>
        /// This method should only be called from the Single Page Index view, using ChildActionOnly stops this view from being called by the url
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [ChildActionOnly]
        [Route]
        public ActionResult Index()
        {
            var model = new QuoteRequest();

            return View("Index", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// [ Security - CSRF ]
        /// [ ValidateAntiForgeryToken() ] - Prevents MVC Cross Site Request Forgery
        /// This has to be used with @Html.AntiForgeryToken() on the form
        /// </remarks>
        [HttpPost]
        [ValidateAntiForgeryHeader]        
        [EventAnalysis(Category = "User Interaction", Action = "Quote", Label = "Quote Request", Value = "0")]
        [Route]        
        public ActionResult Index(QuoteRequest model)
        {
            if (ModelState.IsValid)
            {                
                var emailBody = RenderViewToString(Email.EmailView, ViewData, ControllerContext, model, "Quote", "Email");

                var notificationType = model.SendEmail(emailBody, HttpContext) ? NotificationType.Success : NotificationType.Failure;                

                // Create a new controller rather than using a redirect, a redirect will terminate the http request and return a 302
                // A 302 response will break the ajax method that called this function
                // Use this method of getting the controller as creating a new controller object will cause issues
                var notificationController = DependencyResolver.Current.GetService<NotificationController>();

                // The controller context needs to be created and added to the controller for it to function
                notificationController.ControllerContext = new ControllerContext(Request.RequestContext, notificationController);

                // Add the temp data directly to the controller since we're not passing through the mechanism that would copy this for us
                notificationController.TempData["NotificationType"] = notificationType;

                // Call the action method and return the result
                return notificationController.Index();
            }

            var data = RenderViewToString("Index", ViewData, ControllerContext, model, "Quote", "Index");

            return Json(new { view = data, target = "quote" }, JsonRequestBehavior.AllowGet);
        }
    }
}