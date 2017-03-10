using System.Web.Mvc;

namespace Portal.CMS.Web.Controllers
{
    public class ExampleController : Controller
    {
        [ChildActionOnly]
        public ActionResult ExamplePartial()
        {
            return View("_ExamplePartial");
        }
    }
}