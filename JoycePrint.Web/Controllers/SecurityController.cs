using System.Web.Mvc;
using JoycePrint.Domain.Models;
using JoycePrint.Domain.Security;

namespace JoycePrint.Web.Controllers
{
    [RoutePrefix("security")]
    public class SecurityController : BaseController
    {
        /// <summary>
        /// This returns the recaptcha security view
        /// This is only callable from the server side
        /// </summary>
        /// <returns></returns>                
        [HttpGet]
        [ChildActionOnly]
        [Route("recaptcha")]
        public ActionResult Recaptcha()
        {
            var model = new Security();

            return PartialView("Recaptcha", model);
        }

        [HttpPost]
        [Route("recaptcha")]
        public ActionResult ProcessRecaptcha(string captchaResponse)
        {
            var recaptcha = new Recaptcha();

            var result = recaptcha.Verify(captchaResponse);

            return Json(result);
        }
    }
}