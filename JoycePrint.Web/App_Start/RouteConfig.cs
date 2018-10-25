using System.Web.Mvc;
using System.Web.Routing;

namespace JoycePrint.Web
{
    public static class RouteConfig
    {
        private static readonly string[] JoycePrintNamespace = { "JoycePrint.Web.Controllers" };

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Enabling attribute routing
            routes.MapMvcAttributeRoutes();

            // This is the catch all route
            routes.MapRoute(
                name: "CatchAll",
                url: "{*url}",
                defaults: new { controller = "Error", action = "NotFound" },
                namespaces: JoycePrintNamespace
            );
        }
    }
}