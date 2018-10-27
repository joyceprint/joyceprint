using System.Web.Mvc;

namespace DocketBooks.Web.Controllers
{
    [Route]
    public class SinglePageController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}