using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Areas.Builder.ViewModels.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class ThemeController : Controller
    {
        #region Dependencies

        private readonly IThemeService _themeService;

        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        #endregion Dependencies

        // GET: Builder/Styles
        public ActionResult Render()
        {
            var model = new ThemeViewModel
            {
                Theme = _themeService.GetDefault()
            };

            return View(model);
        }
    }
}