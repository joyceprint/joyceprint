using System.Collections.Generic;
using System.Web.Optimization;

namespace DocketBooks.Web
{
    public static class BundleConfig
    {
        #region Bundle Files

        /// <summary>
        /// The list of base scripts to be used on each page
        /// </summary>
        private static List<string> BaseBundle => new List<string> {
            "~/Scripts/jquery-3.1.1.min.js",
            "~/Scripts/materialize.min.js",
            "~/Scripts/docketbooks/jLib-loading.js",
            "~/Scripts/docketbooks/jLib-materialize-extensions.js",
            "~/Scripts/docketbooks/jLib-nav.js",
            "~/Scripts/docketbooks/docketbooks.js"
        };

        #endregion

        /// <summary>
        /// Register the bundles for the initial application start up
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            AddStyleBundles(bundles);

            AddScriptBundles(bundles);

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
        private static void AddScriptBundles(BundleCollection bundles)
        {
            var baseBundle = new ScriptBundle("~/js/docketbooksjs")
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
            bundles.Add(new Bundle("~/css/docketbookscss",
                                new IBundleTransform[] { new LessTransform(), new CssMinify() })
                                .Include("~/Content/css/materialize.min.css",
                                    "~/Content/css/docketbooks.less"));
        }
    }
}