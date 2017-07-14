using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using Common.Logging.Enums;

namespace JoycePrint.Web
{
    public class MvcApplication : HttpApplication
    {        
        protected void Application_Start()
        {
            EnableSecurity();

            // Add the updates to the view engine so we can define our own partial view paths
            ViewEngines.Engines.Add(new ViewEngine());

            AreaRegistration.RegisterAllAreas();

            // Register the filters globally for the application
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Register the routes used for the application
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register the bundles used in the application
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Handle the error case when the request is too big.
        /// This will indicate that the user is trying to upload a file that is too large
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var error = HttpContext.Current.Error as HttpException;
            var httpException = error;

            if (httpException?.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
            {
                Logger.Instance.Log(MessageLevel.Error, $"File Upload Size Exception - {httpException.Message} - Inner Exception - {httpException.InnerException}");

                Server.ClearError();

                Response.RedirectToRoute("UploadSize");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Response Headers
        /// --------------------------------------------------------------------------------------------
        /// Server: added by IIS. [ TODO: NOT DONE YET ]
        /// X-AspNet-Version: added by System.Web.dll at the time of Flush in HttpResponse class [ web.config ]
        /// X-AspNetMvc-Version: Added by MvcHandler in System.Web.dll. [ MvcApplication.EnableSecurity ]
        /// X-Powered-By: added by IIS [ web.config ]
        /// AntiForgery Token: Change the name of the antiforgery token to obscure that we're using .NET
        /// </remarks>
        private static void EnableSecurity()
        {
            // [ Security - Click Jack Attack via IFrame ]
            // This adds the X-FRAME-OPTIONS : DENY | SAMEORIGIN to the reponse.
            // This prevents click hyjacking by preventing the page from being loaded into an iframe
            // This is on by default in MVC 5, we have it here incase we ever change versions
            AntiForgeryConfig.SuppressXFrameOptionsHeader = false;

            // [Security - Application Hardening ]
            // Removing X-AspNetMvc-Version - Indicates that the website is "powered by MVC Version."
            MvcHandler.DisableMvcResponseHeader = true;

#if DEBUG
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
            MvcHandler.DisableMvcResponseHeader = false;
#endif

            //ConfigureAntiForgeryTokens();
        }

        private static void ConfigureAntiForgeryTokens()
        {
            // [Security - Application Hardening ]
            // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "__st". 
            // This adds a little security through obscurity and also saves sending a few characters over the wire.
            AntiForgeryConfig.CookieName = "__st";

            // If you have enabled SSL. Uncomment this line to ensure that the Anti-Forgery cookie requires SSL to be sent accross the wire. 
            // AntiForgeryConfig.RequireSsl = true;
        }
    }
}
