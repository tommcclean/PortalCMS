using System.Web.Optimization;

namespace Portal.CMS.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            #region Script Bundles

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js").Include("~/Content/Scripts/Bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap/Popover").Include("~/Content/Scripts/Bootstrap/initialise.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap/Confirmation").Include("~/Content/Scripts/BootstrapConfirmation/bootstrap-confirmation.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap/Modal").Include("~/Content/Scripts/Framework/modal.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Framework/Global").Include("~/Content/Scripts/Framework/global.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/FancyBox").Include("~/Content/Scripts/FancyBox/jquery.fancybox.js").Include("~/Content/Scripts/FancyBox/jquery.fancybox-thumbs.js").Include("~/Content/Scripts/FancyBox/initialise.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/C3Graphing").Include("~/Content/Scripts/C3Graphing/c3.min.js").Include("~/Content/Scripts/C3Graphing/d3.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/JQueryTouch").Include("~/Content/Scripts/JQueryTouch/jquery.ui.touch-punch.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/TinyMCE").Include("~/Content/Scripts/TinyMCE/tinymce.min.js").Include("~/Content/Scripts/TinyMCE/initialise.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/ImageSelector").Include("~/Content/Scripts/ImageSelector/*.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/PageBuilderAdministration").Include("~/Areas/Builder/Content/Scripts/pagebuilder.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/FAQSpoilerEvents").Include("~/Areas/Builder/Content/Scripts/component.expand.js"));

            #endregion Script Bundles

            #region Style Bundles

            bundles.Add(new StyleBundle("~/Resources/CSS/Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css").Include("~/Content/Styles/Bootstrap/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/FancyBox").Include("~/Content/Styles/FancyBox/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/C3Graphing").Include("~/Content/Styles/C3Graphing/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/JQuery/JQueryUI").Include("~/Content/Styles/JQueryUI/jquery-ui.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/FontAwesome", "https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css").Include("~/Content/Styles/FontAwesome/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Framework").Include("~/Content/Styles/Framework/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/PageBuilder/Framework").Include("~/Areas/Builder/Content/Styles/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Administration/Framework").Include("~/Areas/Admin/Content/Styles/Framework/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Misc/Dashboard").Include("~/Areas/Admin/Content/Styles/Pages/dashboard.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Misc/Portal").Include("~/Areas/Admin/Content/Styles/Pages/portal.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Misc/Posts").Include("~/Areas/Admin/Content/Styles/Pages/posts.css", new CssRewriteUrlTransform()));

            #endregion Style Bundles
        }
    }
}