using Portal.CMS.Web.Architecture.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web.Architecture.ActionFilters
{
    public sealed class EditorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserHelper.IsEditor)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Home" }, { "area", "" } });
        }
    }
}