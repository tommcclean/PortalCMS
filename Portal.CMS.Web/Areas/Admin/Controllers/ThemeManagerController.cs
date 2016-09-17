using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager;
using System;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter]
    public class ThemeManagerController : Controller
    {
        #region Dependencies

        private readonly IThemeService _themeService;
        private readonly IFontService _fontService;

        public ThemeManagerController(IThemeService themeService, IFontService fontService)
        {
            _themeService = themeService;
            _fontService = fontService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            var model = new ThemeViewModel
            {
                Themes = _themeService.Get(),
                Fonts = _fontService.Get()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new UpsertViewModel
            {
                FontList = _fontService.Get()
            };

            return PartialView("_Create", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UpsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FontList = _fontService.Get();

                return PartialView("_Create", model);
            }

            _themeService.Upsert(0, model.ThemeName, model.TitleFontId, model.TextFontId);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int themeId)
        {
            var theme = _themeService.Get(themeId);

            if (theme == null)
                throw new ArgumentException(string.Format("Unable to Identify Theme: {0}", themeId));

            var model = new UpsertViewModel
            {
                ThemeId = themeId,
                ThemeName = theme.ThemeName,
                TextFontId = theme.TextFontId.Value,
                TitleFontId = theme.TitleFontId.Value,
                FontList = _fontService.Get(),
                IsDefault = theme.IsDefault
            };

            return PartialView("_Edit", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UpsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FontList = _fontService.Get();

                return PartialView("_Edit", model);
            }

            _themeService.Upsert(model.ThemeId, model.ThemeName, model.TitleFontId, model.TextFontId);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Default(int themeId)
        {
            return View("_Default", new DefaultViewModel { ThemeId = themeId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Default(DefaultViewModel model)
        {
            _themeService.Default(model.ThemeId);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int themeId)
        {
            _themeService.Delete(themeId);

            return RedirectToAction(nameof(Index));
        }
    }
}