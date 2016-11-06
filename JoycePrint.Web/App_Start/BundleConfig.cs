using System.Web.Optimization;

namespace JoycePrint.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            AddBundles(bundles);

            // Optimization for script and style bundles.
            // In debug mode the scripts will not be minified in the browser
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }

        private static void AddBundles(BundleCollection bundles)
        {
            // Create the script bundle
            // {version} can be used to specify the jQuery version, the token has to appear last in the file name
            bundles.Add(new ScriptBundle("~/js/joyceprintjs")
                .Include("~/Scripts/jquery-{version}.js",
                    "~/Scripts/materialize.min.js",
                    "~/Scripts/joyceprint/joyceprint.js"
                ));

            // Create the external style bundle, using a bundle instead of a style bundle allows us to mix css and less files.
            // The less file is translated to css by the LessTransform class passed into the bundle
            bundles.Add(new Bundle("~/css/joyceprintcss",
                new IBundleTransform[] { new LessTransform(), new CssMinify() })
                .Include("~/Content/css/materialize.min.css",
                    "~/Content/css/joyceprint/joyceprint.less")
                );
        }

        private static void AddBundlesWithCdn(bool useCdn, BundleCollection bundles)
        {
            var MaterializeStyleCdn = @"https://cdnjs.cloudflare.com/ajax/libs/materialize/0.97.8/css/materialize.min.css";
            var MaterializeJsCdn = @"https://cdnjs.cloudflare.com/ajax/libs/materialize/0.97.8/js/materialize.min.js";

            bundles.UseCdn = true;

            // Create the internal script bundle
            bundles.Add(new ScriptBundle("~/js/joyceprintjs")
                .Include("~/Scripts/joyceprint/joyceprint.js")
                );

            // Create the external script bundle
            // {version} can be used to specify the jQuery version, the token has to appear last in the file name
            bundles.Add(new ScriptBundle("~/js/libraryjs", MaterializeJsCdn).
                Include("~/Scripts/jquery-{version}.js",
                    "~/Scripts/materialize.min.js")
                );

            // Create the internal style bundle, using a bundle instead of a style bundle allows us to mix css and less files.
            // The less file is translated to css by the LessTransform class passed into the bundle
            bundles.Add(new Bundle("~/css/joyceprintcss",
                    new IBundleTransform[] { new LessTransform(), new CssMinify() })
                    .Include("~/Content/css/joyceprint/joyceprint.less")
                );

            // Create the external style bundle, using a bundle instead of a style bundle allows us to mix css and less files.
            // The less file is translated to css by the LessTransform class passed into the bundle
            bundles.Add(new Bundle("~/css/librarycss", MaterializeStyleCdn,
                    new IBundleTransform[] { new CssMinify() })
                    .Include("~/Content/css/materialize.min.css")
                );
        }
    }
}