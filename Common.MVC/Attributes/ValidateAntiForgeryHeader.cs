using System;
using System.Web.Mvc;

namespace Common.MVC.Attributes
{
    [Obsolete("This functionality is being handled by the built in class ValidateAntiForgeryToken")]
    public class ValidateAntiForgeryHeader : FilterAttribute, IAuthorizationFilter
    {        
        /// <summary>
        /// Security - Application Hardening
        /// Rename the anti forgery token to hide that fact that you're running MVC
        /// </summary>
        //private const string KeyName = "__st";
        // TODO: Changing this header is supposed to work - try it again
        private const string KeyName = "__RequestVerificationToken";

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var clientToken = filterContext.RequestContext.HttpContext.Request.Headers.Get(KeyName);

            if (clientToken == null)
                throw new HttpAntiForgeryException($"Header does not contain {KeyName}");

            var serverToken = filterContext.HttpContext?.Request?.Cookies?.Get(KeyName)?.Value;

            if (serverToken == null)
                throw new HttpAntiForgeryException($"Cookies does not contain {KeyName}");

            System.Web.Helpers.AntiForgery.Validate(serverToken, clientToken);
        }
    }
}