using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication
{
    public class AuthenticationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Authentication";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Authentication_default",
                "Authentication/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}