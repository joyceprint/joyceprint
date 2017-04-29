using System.Web.Mvc;

namespace JoycePrint.Web
{
    public static class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <remarks>        
        /// HandleErrorAttribute
        /// -------------------------
        /// This captures any error thrown in the ASP.NET MVC pipeline and returns a custom "Error" view providing you have custom 
        /// errors enabled in web.config. 
        /// It will look for this view in these locations
        /// ~/views/{controllerName}/error.cshtml 
        /// ~/views/shared/error.cshtml.
        /// 
        /// Any exceptions thrown outside of the MVC pipeline will fall back to the standard ASP.NET error pages configuration.
        /// 
        /// HandleErrorAttribute can be removed as it will be handled by the IIS error handling section <httpErrors></httpErrors>
        /// </remarks>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {            
            //filters.Add(new HandleErrorAttribute());
        }
    }
}