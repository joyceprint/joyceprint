using System.Web.Configuration;
using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [RoutePrefix("")]
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Handles errors thrown from requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This is called from
        /// --> No where at the moment
        /// </remarks>        
        [HttpGet]
        [Route("error", Name = "Error")]
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
        /// <remarks>
        /// This is redirected to from 
        /// --> The base controller OnExecption method
        /// </remarks>        
        [HttpGet]
        [Route("exception", Name = "Exception")]
        public ActionResult Exception()
        {
            // Set this value to hide the navigation menu in the _Navigation view
            TempData["HideNavMenuOnError"] = true;

            // Get the handle error info model to display on the page if it's being run in debugging mode
            var model = TempData["errorInfo"];

            return View("Error/Exception", model);
        }

        /// <summary>
        /// Handles a 404 from inside the MVC Request Handler
        /// </summary>
        /// <returns></returns>        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [Route("notfound", Name = "NotFound")]
        public ActionResult NotFound()
        {
            // Set this value to hide the navigation menu in the _Navigation view
            TempData["HideNavMenuOnError"] = true;

            // Returning a view here will result in the client getting a 200 status code.
            // Set the Response.StatusCode to 404 to cause the pipeline to use the error page referenced
            // by the HttpErrors section of the config file for a 404 response to preserve the 404 error code      
            //Response.StatusCode = 404;

            return View("Error/NotFound");
        }

        /// <summary>
        /// Handles errors thrown from ajax requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This is invoked from 
        /// --> The base controller OnExecption method
        /// --> The javascript method HandleAjaxError in the error.js file        
        /// </remarks>        
        [HttpGet]
        [Route("ajax", Name = "Ajax")]
        public ActionResult Ajax()
        {
            DisableAjaxRequestCachingInInternetExplorer();

            var data = RenderPartialViewToString(ControllerContext, "Error/Ajax", null);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Handles the case where the user uploads a file that is too large to be sent using the site
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("uploadsize", Name = "UploadSize")]
        public ActionResult UploadSize()
        {
            DisableAjaxRequestCachingInInternetExplorer();

            var runTime = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");

            // Approx 100 Kb (for page content) size has been deducted because the maxRequestLength proprty is the page size,
            // not only the file upload size. Size in MB           
            var maxRequestLength = (runTime.MaxRequestLength - 100) / 1024;

            TempData["maxRequestLength"] = maxRequestLength;

            var data = RenderPartialViewToString(ControllerContext, "Error/UploadSize", null);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }
    }
}