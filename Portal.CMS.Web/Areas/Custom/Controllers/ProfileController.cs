using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Custom.Controllers
{
    [ChildActionOnly]
    public class ProfileController : Controller
    {
        public ActionResult Manage()
        {
            return View();
        }
    }
}