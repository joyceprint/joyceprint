﻿using System.Web.Mvc;
using JoycePrint.Domain.Security;

namespace JoycePrint.UI.Controllers
{    
    [Route("security")]
    public class SecurityController : BaseController
    {
        [Route("security/recaptcha")]
        [HttpPost]        
        public ActionResult ProcessRecaptcha(string captchaResponse)
        {
            var recaptcha = new Recaptcha(Config);
            var result = recaptcha.Verify(captchaResponse);

            return Json(result);
        }       
    }
}