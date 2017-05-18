﻿using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JoycePrint.Web
{
    public class MvcApplication : System.Web.HttpApplication
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
        /// 
        /// </summary>
        /// <remarks>
        /// Response Headers
        /// --------------------------------------------------------------------------------------------
        /// Server: added by IIS. [ TODO: Unhandled ]
        /// X-AspNet-Version: added by System.Web.dll at the time of Flush in HttpResponse class [ web.config ]
        /// X-AspNetMvc-Version: Added by MvcHandler in System.Web.dll. [ MvcApplication.EnableSecurity ]
        /// X-Powered-By: added by IIS [ TODO: Unhandled ]
        /// </remarks>
        private void EnableSecurity()
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
