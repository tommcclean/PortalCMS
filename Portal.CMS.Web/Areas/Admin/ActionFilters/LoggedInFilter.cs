using Portal.CMS.Web.Areas.Admin.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web.Areas.Admin.ActionFilters
{
    public class LoggedInFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserHelper.IsLoggedIn == false)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "Index" }, { "controller", "Home" }, { "area", "" }
                });
            }
        }
    }
}