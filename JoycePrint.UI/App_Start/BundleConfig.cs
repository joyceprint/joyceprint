using System.Web.Optimization;

namespace JoycePrint.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            // Create the script bundle
            // {version} can be used to specify the jQuery version, the token has to appear last in the file name
            bundles.Add(new ScriptBundle("~/Scripts/joyceprintjs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/joyceprint/joyceprint.js"));

            // Create the style bundle, using a bundle instead of a style bundle allows us to mix css and less files.
            // The less file is translated to css by the LessTransform class passed into the bundle
            bundles.Add(new Bundle("~/Content/css/joyceprintcss", new LessTransform()).Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/bootstrap-theme.min.css",
                "~/Content/css/joyceprint/joyceprint.less"
                ));
            
            // Optimization for script and style bundles.
            // In debug mode the scripts will not be minified in the browser
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}