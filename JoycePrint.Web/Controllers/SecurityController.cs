using System.Web.Mvc;
using JoycePrint.Domain.Models;
using JoycePrint.Domain.Security;

namespace JoycePrint.Web.Controllers
{    
    [Route("security")]
    public class SecurityController : BaseController
    {
        [Route("security/recaptcha")]
        [HttpGet]
        public ActionResult Recaptcha()
        {            
            var model = new Security();

            return PartialView("Recaptcha", model);
        }

        [Route("security/recaptcha")]
        [HttpPost]        
        public ActionResult ProcessRecaptcha(string captchaResponse)
        {
            var recaptcha = new Recaptcha();
            var result = recaptcha.Verify(captchaResponse);

            return Json(result);
        }       
    }
}