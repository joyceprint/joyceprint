using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DocketBooks.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EnableSecurity();

            // Add the updates to the view engine so we can define our own partial view paths
            ViewEngines.Engines.Add(new ViewEngine());

            AreaRegistration.RegisterAllAreas();
            
            // Register the routes used for the application
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register the bundles used in the application
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
        }      
    }
}
