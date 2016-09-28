using Portal.CMS.Web.Areas.Admin.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web.Architecture.ActionFilters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserHelper.IsAdmin == false)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "Index" }, { "controller", "Home" }, { "area", "" }
                });
            }
        }
    }
}