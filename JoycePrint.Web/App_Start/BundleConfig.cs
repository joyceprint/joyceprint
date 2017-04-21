using System.Collections.Generic;
using System.Web.Optimization;
using JoycePrint.Web.Extensions;

namespace JoycePrint.Web
{
    // TODO: Static classes here will cause an issue - fix this
    // TODO: ensure this is not screwing up anything

    public static class BundleConfig
    {
        /// <summary>
        /// Page enum to specific which additional script bundles are required
        /// </summary>
        public enum PageBundle
        {
            None,
            Home,
            Services,            
            AboutUs,
            ContactUs,
            Quote,
            All
        }

        /// <summary>
        /// The list of base bundles to be used on each page
        /// </summary>
        public static List<string> BaseBundle => new List<string> {
            "~/Scripts/jquery-3.1.1.js",
            "~/Scripts/materialize.min.js",
            "~/Scripts/joyceprint/materialize-extensions.js",
            "~/Scripts/joyceprint/joyceprint-extensions.js",
            "~/Scripts/joyceprint/joyceprint-nav.js",
            "~/Scripts/joyceprint/joyceprint-loading.js",
            "~/Scripts/joyceprint/joyceprint.js",            
            "~/Scripts/joyceprint/error.js"
        };

        /// <summary>
        /// Register the bundles for the initial application start up
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            AddStyleBundles(bundles);

            AddScriptComposableBundles(bundles);

            // Optimization for script and style bundles.
            // In debug mode the scripts will not be minified in the browser
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }        

        /// <summary>
        /// Add the scripts to a composable bundle
        /// </summary>
        /// <param name="bundles"></param>
        private static void AddScriptComposableBundles(BundleCollection bundles)
        {
            var baseBundle = new ScriptBundle("~/js/joyceprintjs")
                                            .AsComposable()
                                            .Include(BaseBundle.ToArray());
                        
            bundles.Add(baseBundle);
        }        

        /// <summary>
        /// Add the styles to the style bundle
        /// </summary>
        /// <param name="bundles"></param>
        private static void AddStyleBundles(BundleCollection bundles)
        {
            // Create the external style bundle, using a bundle instead of a style bundle allows us to mix css and less files.
            // The less file is translated to css by the LessTransform class passed into the bundle
            bundles.Add(new Bundle("~/css/joyceprintcss",
                                new IBundleTransform[] { new LessTransform(), new CssMinify() })
                                .Include("~/Content/css/materialize.min.css",                                    
                                    "~/Content/css/joyceprint.less")
                                    );
        }

        /// <summary>
        /// Update the base script bundle based on the page bundle passed in
        /// </summary>
        public static void UpdateScriptBundle(Bundle baseBundle, PageBundle pageBundle)
        {
            var additionalScript = new List<string>();

            switch(pageBundle)
            {
                case PageBundle.None: break;
                case PageBundle.Home: 
                    break;
                case PageBundle.Services:
                    break;
                case PageBundle.AboutUs:                   
                    break;
                case PageBundle.ContactUs:                    
                    break;
                case PageBundle.Quote:
                    additionalScript.Add("~/Scripts/joyceprint/jalidate.js");              
                    additionalScript.Add("~/Scripts/joyceprint/joyceprint-quote.js");
                    additionalScript.Add("~/Scripts/joyceprint/joyceprint-recaptcha.js");
                    additionalScript.Add("~/Scripts/joyceprint/joyceprint-notification.js");
                    break;  
                case PageBundle.All:                    
                    additionalScript.Add("~/Scripts/joyceprint/jalidate.js");
                    additionalScript.Add("~/Scripts/joyceprint/joyceprint-quote.js");
                    additionalScript.Add("~/Scripts/joyceprint/joyceprint-recaptcha.js");
                    additionalScript.Add("~/Scripts/joyceprint/joyceprint-notification.js");
                    break;
            }

            var additionalBundle = new Bundle("~/js/validationjs")
                                    .AsComposable()
                                    .Include(additionalScript.ToArray());

            baseBundle.AsComposable().UseBundle(additionalBundle);
        }
    }
}