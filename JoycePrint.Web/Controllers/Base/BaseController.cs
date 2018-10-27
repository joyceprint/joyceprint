using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Logging;
using Common.Logging.Enums;

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

        #region Error Handling

        /// <summary>
        /// Override the OnException method to handle all MVC exceptions that can occur.
        /// This allows us to distinguish between regular and AJAX requests
        /// 
        /// IIS --> ASP --> [ MVC ]
        /// </summary>
        /// <param name="filterContext"></param>
        /// <remarks>
        /// Handles all exceptions that are thrown within the MVC framework
        /// 
        /// No exception that originates outside the controller will be caught by OnException. 
        /// An excellent example of an exception not being caught by OnException is a ‘null reference’ 
        /// exception that results in the model-binding layer. 
        /// Another example is ‘route not-found’ exception.
        /// 
        /// The Controller.OnException method gives you a little bit more flexibility than the HandleErrorAttribute, 
        /// but it is still tied to the MVC framework. It is useful when you need to distinguish your error handling 
        /// between regular and AJAX requests on a controller level.
        /// </remarks>
        protected override void OnException(ExceptionContext filterContext)
        {
            CreateErrorInfo(filterContext);

            // Set the exception to handled to stop if from bubbling out of the MVC block
            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = filterContext.HttpContext.Request.IsAjaxRequest() ? RedirectForAjax() : RedirectToAction("Exception", "Error");
        }

        /// <summary>
        /// We call directly into the ajax error action method and return it's result to stop the server from returning a 302 code
        /// and causing the client to have to handle a follow up ajax request
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectForAjax()
        {
            // Create a new controller rather than using a redirect, a redirect will terminate the http request and return a 302
            // A 302 response will break the ajax method that called this function
            // Use this method of getting the controller as creating a new controller object will cause issues
            var errorController = DependencyResolver.Current.GetService<ErrorController>();

            // The controller context needs to be created and added to the controller for it to function
            errorController.ControllerContext = new ControllerContext(Request.RequestContext, errorController);

            // Call the action method and return the result
            return errorController.Ajax();
        }

        /// <summary>
        /// Create the HandleErrorInfo model from the filterContext
        /// </summary>
        /// <param name="filterContext"></param>
        private void CreateErrorInfo(ExceptionContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"]?.ToString() ?? "Unknown Action";

            // Log the exception
            Logger.Instance.Log(MessageLevel.Error, filterContext.Exception, GetContextInfo(filterContext));

            // Create the error model
            var errorInfo = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            // Save the exception so it can be displayed on the view
            TempData["errorInfo"] = errorInfo;
        }

        /// <summary>
        /// Checks the context of the exception and returns the relevant information
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private string GetContextInfo(ExceptionContext filterContext)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Exception Context Additional Information");
            sb.AppendLine($"Child Action: [{filterContext.IsChildAction}]");
            sb.AppendLine($"Ajax Request: [{filterContext.HttpContext.Request.IsAjaxRequest()}]");

            return sb.ToString();
        }

        #endregion
    }
}