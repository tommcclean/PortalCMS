using Portal.CMS.Web.Architecture.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web.Architecture.ActionFilters
{
    public sealed class EditorFilter : ActionFilterAttribute
    {
        private ActionFilterResponseType responseType;

        public EditorFilter(ActionFilterResponseType modal)
        {
            this.responseType = modal;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserHelper.IsEditor)
            {
                if (responseType == ActionFilterResponseType.Page)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Home" }, { "area", "" } });
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Authentication" }, { "area", "Admin" } });
                }
            }
        }
    }
}