using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

// ReSharper disable once CheckNamespace
namespace JoycePrint.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Disable caching [Internet Explorer]
        /// This is required, otherwise all ajax requests will be cached        
        /// </summary>
        public void DisableAjaxRequestCachingInInternetExplorer()
        {
            HttpContext.Response.AddHeader("Cache-Control", "no-cache,no-store");
        }

        /// <summary>
        /// Renders a razor view to a string
        /// </summary>
        /// <param name="viewToRender">The name of the view</param>
        /// <param name="viewData">The view data to pass into the view</param>
        /// <param name="controllerContext">The controller context used to set the model passed into the view</param>
        /// <param name="model">The model that will be passed through the controller context to the view</param>
        /// <param name="callingController">The controller calling this method, this is required for updating the context</param>
        /// <param name="callingAction">The action calling this method, this is required for updating the context</param>
        /// <returns>The entire view as a string</returns>
        public string RenderViewToString(string viewToRender, ViewDataDictionary viewData, ControllerContext controllerContext, object model, string callingController, string callingAction)
        {
            using (var output = new StringWriter())
            {
                controllerContext.Controller.ViewData.Model = model;

                var result = ViewEngines.Engines.FindView(controllerContext, viewToRender, null);

                var viewContext = new ViewContext(controllerContext, result.View, viewData, controllerContext.Controller.TempData, output)
                {
                    RouteData = UpdateContext(controllerContext, callingController, callingAction)
                };

                result.View.Render(viewContext, output);
                result.ViewEngine.ReleaseView(controllerContext, result.View);

                return output.ToString();
            }
        }

        /// <summary>
        /// Updates the contoller context to reset the controller and action that will be used
        /// This is used to setup the correct route for the [RenderViewToString] function
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns>Route data containing the controller and view</returns>
        private RouteData UpdateContext(ControllerContext controllerContext, string controllerName, string actionName)
        {
            var routeData = new RouteData();

            // - change this
            routeData.Values.Add("controller", controllerName);
            routeData.Values.Add("action", actionName);

            routeData.RouteHandler = controllerContext.RouteData.RouteHandler;
            routeData.Route = controllerContext.RouteData.Route;

            return routeData;
        }

        /// <summary>
        /// Renders a partial razor view to a string
        /// </summary>
        /// <param name="controllerContext">The controller context used to set the model passed into the view</param>
        /// <param name="viewName">The name of the patial view to render</param>
        /// <param name="model">The model that will be passed through the controller context to the view</param>
        /// <returns>The partial view as a string</returns>
        /// <remarks>Currently not being used</remarks>
        public string RenderPartialViewToString(ControllerContext controllerContext, string viewName, object model)
        {
            using (var sw = new StringWriter())
            {
                controllerContext.Controller.ViewData.Model = model;

                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);

                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }
    }
}