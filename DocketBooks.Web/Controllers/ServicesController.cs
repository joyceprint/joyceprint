using System.Web.Mvc;

namespace DocketBooks.Web.Controllers
{
    [RoutePrefix("services")]
    public class ServicesController : BaseController
    {
        /// <summary>
        /// This method should only be called from the Single Page Index view, using ChildActionOnly stops this view from being called by the url
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [Route]
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// This method should only be called from the Single Page Index view, using ChildActionOnly stops this view from being called by the url
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [Route]
        [ChildActionOnly]
        public ActionResult DocketBooks()
        {
            return View("DocketBooks");
        }
    }
}