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
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Ajax()
        {
            var data = RenderPartialViewToString(ControllerContext, null, null);

            return Json(new { modalView = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Handles errors thrown from general requests within the MVC framework
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult General()
        {
            return View("Error/General");
        }
    }
}