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
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Ajax()
        {
            //todo this needs to handle all ajax screw ups
            var data = RenderPartialViewToString(ControllerContext, null, null);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Handles errors thrown from general requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        [Route("error/general")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult General()
        {
            // Set this value to hide the navigation menu in hte _Navigation view
            TempData["HideNavMenuOnError"] = true;

            return View("Error/Error");
        }
    }
}