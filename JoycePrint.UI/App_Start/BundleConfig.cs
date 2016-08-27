using System.Web.Optimization;

namespace JoycePrint.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/Scripts/bootstrapjs").Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrapcss").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/bootstrap-theme.min.css"
                ));

            bundles.Add(new LessBundle("~/Content/css/joyceprint/joyceprintless").Include(
                "~/Content/css/joyceprint/joyceprint.less"));

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