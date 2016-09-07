using System.Web.Optimization;

namespace Portal.CMS.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            #region Script Bundles

            //bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap/Confirmation")
                .Include("~/Areas/Admin/Content/Plugins/Confirmation/bootstrap-confirmation.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap/Modal")
                .Include("~/Areas/Admin/Content/Scripts/modal.min.js"));

            #endregion Script Bundles

            #region Style Bundles

            //bundles.Add(new StyleBundle("~/Resources/CSS/Bootstrap").Include(
            //    "~/Content/styles/plugins/bootstrap-notify/*.css", new CssRewriteUrlTransform()));

            //bundles.Add(new StyleBundle("~/Resources/CSS/JQueryUI").Include(
            //    "~/Content/styles/plugins/bootstrap-popover/*.css", new CssRewriteUrlTransform()));

            #endregion Style Bundles
        }
    }
}