using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.Profile.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class WidgetsController : Controller
    {
        public ActionResult ProfileWidget()
        {
            return PartialView("_ProfileWidget");
        }

        public ActionResult BioWidget()
        {
            return PartialView("_BioWidget");
        }

        public ActionResult SecurityWidget()
        {
            return PartialView("_SecurityWidget");
        }
    }
}