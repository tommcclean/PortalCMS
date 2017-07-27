using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class ThemeManagerController : Controller
    {
        #region Dependencies

        readonly IThemeService _themeService;
        readonly IFontService _fontService;

        public ThemeManagerController(IThemeService themeService, IFontService fontService)
        {
            _themeService = themeService;
            _fontService = fontService;
        }

        #endregion Dependencies

        [AdminFilter]
        public ActionResult Index()
        {
            var model = new ThemeViewModel
            {
                Themes = _themeService.Get(),
                Fonts = _fontService.Get()
            };

            return View(model);
        }

        [HttpGet, AdminModalFilter]
        public ActionResult Create()
        {
            var model = new UpsertViewModel
            {
                FontList = _fontService.Get(),
                PageBackgroundColour = "#000000",
                MenuBackgroundColour = "#000000",
                MenuTextColour = "#9d9d9d"
            };

            return PartialView("_Create", model);
        }

        [HttpPost, ValidateAntiForgeryToken, AdminModalFilter]
        public ActionResult Create(UpsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FontList = _fontService.Get();

                return PartialView("_Create", model);
            }

            _themeService.Upsert(0, model.ThemeName, model.TitleFontId, model.TextFontId, model.LargeTitleFontSize, model.MediumTitleFontSize, model.SmallTitleFontSize, model.TinyTitleFontSize, model.TextStandardFontSize, model.PageBackgroundColour, model.MenuBackgroundColour, model.MenuTextColour);

            return Content("Refresh");
        }

        [HttpGet, AdminModalFilter]
        public ActionResult Edit(int themeId)
        {
            var theme = _themeService.Get(themeId);

            if (theme == null)
                throw new ArgumentException($"Unable to Identify Theme: {themeId}");

            var model = new UpsertViewModel
            {
                ThemeId = themeId,
                ThemeName = theme.ThemeName,
                TextFontId = theme.TextFontId.Value,
                TitleFontId = theme.TitleFontId.Value,
                FontList = _fontService.Get(),
                LargeTitleFontSize = theme.TitleLargeFontSize,
                MediumTitleFontSize = theme.TitleMediumFontSize,
                SmallTitleFontSize = theme.TitleSmallFontSize,
                TinyTitleFontSize = theme.TitleTinyFontSize,
                TextStandardFontSize = theme.TextStandardFontSize,
                PageBackgroundColour = theme.PageBackgroundColour,
                MenuBackgroundColour = theme.MenuBackgroundColour,
                MenuTextColour = theme.MenuTextColour,
                IsDefault = theme.IsDefault
            };

            return PartialView("_Edit", model);
        }

        [HttpPost, ValidateAntiForgeryToken, AdminModalFilter]
        public ActionResult Edit(UpsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FontList = _fontService.Get();

                return PartialView("_Edit", model);
            }

            _themeService.Upsert(model.ThemeId, model.ThemeName, model.TitleFontId, model.TextFontId, model.LargeTitleFontSize, model.MediumTitleFontSize, model.SmallTitleFontSize, model.TinyTitleFontSize, model.TextStandardFontSize, model.PageBackgroundColour, model.MenuBackgroundColour, model.MenuTextColour);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Default(int themeId)
        {
            return View("_Default", new DefaultViewModel { ThemeId = themeId });
        }

        [HttpPost, ValidateAntiForgeryToken, AdminFilter]
        public ActionResult Default(DefaultViewModel model)
        {
            _themeService.Default(model.ThemeId);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Delete(int themeId)
        {
            _themeService.Delete(themeId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, AdminFilter]
        public ActionResult AppDrawer()
        {
            var model = new AppDrawerViewModel
            {
                Themes = _themeService.Get(),
                Fonts = new List<Font>()
            };

            model.Fonts.AddRange(model.Themes.Select(x => x.TextFont));
            model.Fonts.AddRange(model.Themes.Select(x => x.TitleFont));

            return View("_AppDrawer", model);
        }
    }
}