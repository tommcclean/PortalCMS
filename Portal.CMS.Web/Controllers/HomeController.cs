using System.Web.Mvc;

namespace Portal.CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [OutputCache(Duration = 86400)]
        public ActionResult Error()
        {
            return View();
        }
    }
}