using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("error")]
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Handles errors thrown from requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        [Route("error/error")]
        [HttpGet]
        public ActionResult Error()
        {
            // Set this value to hide the navigation menu in the _Navigation view
            TempData["HideNavMenuOnError"] = true;            

            return View("Error/Error");
        }

        /// <summary>
        /// Handles exceptions thrown from requests within the MVC framework        
        /// The exception is logged in the OnException method of the base controller class
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("error/exception")]
        public ActionResult Exception()
        {
            // Set this value to hide the navigation menu in the _Navigation view
            TempData["HideNavMenuOnError"] = true;

            var model = TempData["Exception"];

            return View("Error/Exception", model);
        }
    
        /// <summary>
        /// Handles a 404 from inside the MVC Request Handler
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotFound()
        {
            // You may want to set this to 200
            Response.StatusCode = 404;

            return View("Error/NotFound");
        }

        /// <summary>
        /// Handles errors thrown from ajax requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        [Route("error/ajax")]
        [HttpGet]
        public ActionResult Ajax()
        {
            DisableAjaxRequestCachingInInternetExplorer();

            var data = RenderPartialViewToString(ControllerContext, "Error/Ajax", null);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }
    }
}