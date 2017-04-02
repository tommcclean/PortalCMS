using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Profile.Controllers
{
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