using System.Web.Mvc;

namespace JoycePrint.Web.Controllers
{
    [Route("")]
    public class SinglePageController : BaseController
    {
        [HttpGet]        
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}