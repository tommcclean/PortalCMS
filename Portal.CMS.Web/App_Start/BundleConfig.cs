using System.Web.Optimization;

namespace Portal.CMS.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;

            #region Script Bundles

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Framework").Include("~/Content/Scripts/Framework/*.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Framework/Administration").Include("~/Content/Scripts/Administration/*.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js").Include("~/Content/Scripts/Bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/JQueryUI", "https://code.jquery.com/ui/1.12.0-beta.1/jquery-ui.min.js").Include("~/Content/Scripts/JQuery/jquery.ui.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/JQuery", "https://code.jquery.com/jquery-2.2.1.min.js").Include("~/Content/Scripts/JQuery/jquery-2.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/JQueryTouch").Include("~/Content/Scripts/JQuery/jquery.ui.touch-punch.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/FontAwesome").Include("~/Content/Scripts/FontAwesome/fontawesome-iconpicker.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Bootstrap/Plugins/Popover").Include("~/Content/Scripts/Bootstrap/initialise.js").Include("~/Content/Scripts/Bootstrap/bootstrap-confirmation.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/FancyBox").Include("~/Content/Scripts/FancyBox/jquery.fancybox.js").Include("~/Content/Scripts/FancyBox/jquery.fancybox-thumbs.js").Include("~/Content/Scripts/FancyBox/initialise.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/C3Graphing").Include("~/Content/Scripts/C3Graphing/c3.min.js").Include("~/Content/Scripts/C3Graphing/d3.min.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/ImageSelector").Include("~/Content/Scripts/ImageSelector/*.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/FAQ").Include("~/Content/Scripts/Components/component.expand.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/Spectrum").Include("~/Content/Scripts/Spectrum/spectrum.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/Pagination").Include("~/Content/Scripts/Framework/pagination.js"));

            bundles.Add(new ScriptBundle("~/Resources/JavaScript/Plugins/Sliders").Include("~/Content/Scripts/PageBuilder/ThemeManager-Admin.js"));

            #endregion Script Bundles

            #region Style Bundles

            bundles.Add(new StyleBundle("~/Resources/CSS/Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css").Include("~/Content/Styles/Bootstrap/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/FancyBox").Include("~/Content/Styles/FancyBox/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/C3Graphing").Include("~/Content/Styles/C3Graphing/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/JQuery/JQueryUI").Include("~/Content/Styles/JQuery/jquery-ui.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/FontAwesome", "https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css").Include("~/Content/Styles/FontAwesome/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/FontAwesome/Picker").Include("~/Content/Styles/FontAwesome/fontawesome-iconpicker.min.css"));

            bundles.Add(new StyleBundle("~/Resources/CSS/Framework").Include("~/Content/Styles/Framework/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Framework/Administration").Include("~/Content/Styles/Administration/*.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Resources/CSS/Spectrum").Include("~/Content/Styles/Spectrum/spectrum.css"));

            #endregion Style Bundles
        }
    }
}