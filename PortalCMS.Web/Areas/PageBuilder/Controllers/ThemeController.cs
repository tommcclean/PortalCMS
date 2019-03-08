﻿using PortalCMS.Services.Themes;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.PageBuilder.ViewModels.Theme;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PortalCMS.Web.Areas.PageBuilder.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ThemeController : Controller
    {
        readonly IThemeService _themeService;

        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        public ActionResult Render()
        {
            var model = new ThemeViewModel
            {
                Theme = AsyncHelpers.RunSync(() => _themeService.GetDefaultAsync())
            };

            return View(model);
        }
    }
}