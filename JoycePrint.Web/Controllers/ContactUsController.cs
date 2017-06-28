using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [RoutePrefix("contactus")]
    [Route("{action=index}")]
    [ChildActionOnly]
    public class ContactUsController : BaseController
    {
        /// <summary>
        /// This method should only be called from the Single Page Index view, using ChildActionOnly stops this view from being called by the url
        /// </summary>
        /// <returns></returns>        
        [HttpGet]        
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}