using Portal.CMS.Web.Architecture.ViewEngines;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Razor;
using System.Web.Routing;
using System.Web.WebPages;

namespace Portal.CMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Ability to Create Server Side Stylesheets
            ViewEngines.Engines.Add(new CSSViewEngine());
            RazorCodeLanguage.Languages.Add("cscss", new CSharpRazorCodeLanguage());
            WebPageHttpHandler.RegisterExtension("cscss");

            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_Error()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["CustomErrorPage"] == "true")
            {
                Response.Redirect("~/Home/Error");
            }
        }
    }
}