using System.Web.Mvc;

namespace JoycePrint.Web.Attributes
{
    public class ValidateAntiForgeryHeader : FilterAttribute, IAuthorizationFilter
    {
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