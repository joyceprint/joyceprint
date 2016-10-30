using System.Web.Mvc;
using System.Web.Routing;

namespace JoycePrint.UI
{
    public class RouteConfig
    {
        private static readonly string[] JoycePrintNamespace = { "JoycePrint.UI.Controller" };

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: JoycePrintNamespace
            );

            // This is the catch all route
            routes.MapRoute(
                name: "CatchAll",
                url: "{*catchall}",
                defaults: new { controller = "Error", action = "NotFound" },
                namespaces: JoycePrintNamespace
            );
        }
    }
}
