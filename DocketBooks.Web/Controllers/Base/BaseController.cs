using System.Text;
using Common.Logging;
using System.Web.Mvc;
using Common.Logging.Enums;

// ReSharper disable once CheckNamespace
namespace DocketBooks.Web.Controllers
{
    public class BaseController : Controller
    {
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
            filterContext.Result = RedirectToAction("Exception", "Error");
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