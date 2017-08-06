using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ErrorController : Controller
    {
        [OutputCache(Duration = 86400)]
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 86400)]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}