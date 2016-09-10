using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }
    }
}