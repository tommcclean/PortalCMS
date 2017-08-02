using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult SignedOut()
        {
            return View();
        }
    }
}