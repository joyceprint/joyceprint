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
        /// 
        /// This has to accept a post request in the event that the client side validation is turned off
        /// and the server side validation fails
        /// </summary>
        /// <returns></returns>                        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        [Route("recaptcha")]
        public ActionResult Recaptcha()
        {
            var model = new Security();

            return PartialView("Recaptcha", model);
        }

        [HttpPost]
        [Route("processrecaptcha")]
        public ActionResult ProcessRecaptcha(string captchaResponse)
        {
            var recaptcha = new Recaptcha();

            var result = recaptcha.Verify(captchaResponse);

            return Json(result);
        }
    }
}