using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder
{
    public class BuilderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Builder";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Builder_default",
                "Builder/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}