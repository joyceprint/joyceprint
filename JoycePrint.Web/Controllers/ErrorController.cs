using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("error")]
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Handles errors thrown from ajax requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        [Route("error/ajax")]
        [HttpGet]
        public ActionResult Ajax()
        {
            var data = RenderPartialViewToString(ControllerContext, "Error/Ajax", null);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Handles errors thrown from general requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        [Route("error/general")]
        [HttpGet]
        public ActionResult General()
        {
            // Set this value to hide the navigation menu in hte _Navigation view
            TempData["HideNavMenuOnError"] = true;

            return View("Error/Error");
        }
    }
}