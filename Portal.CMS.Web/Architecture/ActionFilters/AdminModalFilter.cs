using Portal.CMS.Web.Architecture.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web.Architecture.ActionFilters
{
    public sealed class AdminModalFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserHelper.IsAdmin)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "SignedOut" }, { "controller", "Error" }, { "area", "PageBuilder" } });
        }
    }
}