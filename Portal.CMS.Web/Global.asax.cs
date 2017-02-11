using LogBook.Services;
using LogBook.Services.Models;
using Portal.CMS.Web.Architecture.Helpers;
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

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new CSSViewEngine());

            RazorCodeLanguage.Languages.Add("cscss", new CSharpRazorCodeLanguage());
            WebPageHttpHandler.RegisterExtension("cscss");

            MvcHandler.DisableMvcResponseHeader = true;

            new LogHandler().WriteLog(LogType.Information, "Website Started Up", string.Empty);
        }

        protected void Application_End()
        {
            new LogHandler().WriteLog(LogType.Information, "Website Shutting Down", string.Empty);
        }

        protected void Application_Error()
        {
            var logHandler = new LogHandler();

            var exception = Server.GetLastError();

            var user = (UserHelper.IsLoggedIn ? $"{UserHelper.GivenName} {UserHelper.FamilyName} {UserHelper.UserId}" : string.Empty);

            logHandler.WriteLog(LogType.Error, "Portal CMS", exception, exception.Message, user);
        }
    }
}