using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("home")]
    public class HomeController : BaseController
    {
        /// <summary>
        /// This method should only be called from the Single Page Index view, using ChildActionOnly stops this view from being called by the url
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}