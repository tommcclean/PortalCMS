using System.Web.Mvc;

namespace Portal.CMS.Web.Architecture.ViewEngines
{
    public class CSSViewEngine : RazorViewEngine
    {
        public CSSViewEngine()
        {
            ViewLocationFormats = new[]
            {
            "~/Views/{1}/{0}.cscss",
            "~/Views/Shared/{0}.cscss",
            "~/Areas/PageBuilder/Views/Theme/{0}.cscss"
        };
            FileExtensions = new[] { "cscss" };
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            controllerContext.HttpContext.Response.ContentType = "text/css";
            return base.CreateView(controllerContext, viewPath, masterPath);
        }
    }
}