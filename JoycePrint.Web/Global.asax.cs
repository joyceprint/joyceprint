using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JoycePrint.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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
    }
}
