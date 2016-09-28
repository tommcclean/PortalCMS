using Portal.CMS.Web.Architecture.Helpers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web.Architecture.ActionFilters
{
    public class LoggedInFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserHelper.IsLoggedIn == false)
            {
                var resetCookie = new HttpCookie("resetCookie");

                if (resetCookie == null)
                {
                    var cookieValues = resetCookie.Value.Split(',');

                    var userId = cookieValues[0];
                    var address = cookieValues[1];
                }

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "Index" }, { "controller", "Home" }, { "area", "" }
                });
            }
        }
    }
}