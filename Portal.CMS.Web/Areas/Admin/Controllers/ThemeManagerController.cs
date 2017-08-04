using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
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

        [AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> Index()
        {
            var model = new ThemeViewModel
            {
                Themes = await _themeService.GetAsync(),
                Fonts = await _fontService.GetAsync()
            };

            return View(model);
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Create()
        {
            var model = new UpsertViewModel
            {
                FontList = await _fontService.GetAsync(),
                PageBackgroundColour = "#000000",
                MenuBackgroundColour = "#000000",
                MenuTextColour = "#9d9d9d"
            };

            return PartialView("_Create", model);
        }

        [HttpPost, ValidateAntiForgeryToken, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Create(UpsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FontList = await _fontService.GetAsync();

                return PartialView("_Create", model);
            }

            await _themeService.UpsertAsync(0, model.ThemeName, model.TitleFontId, model.TextFontId, model.LargeTitleFontSize, model.MediumTitleFontSize, model.SmallTitleFontSize, model.TinyTitleFontSize, model.TextStandardFontSize, model.PageBackgroundColour, model.MenuBackgroundColour, model.MenuTextColour);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Edit(int themeId)
        {
            var theme = await _themeService.GetAsync(themeId);

            if (theme == null)
                throw new ArgumentException($"Unable to Identify Theme: {themeId}");

            var model = new UpsertViewModel
            {
                ThemeId = themeId,
                ThemeName = theme.ThemeName,
                TextFontId = theme.TextFontId.Value,
                TitleFontId = theme.TitleFontId.Value,
                FontList = await _fontService.GetAsync(),
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

        [HttpPost, ValidateAntiForgeryToken, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Edit(UpsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FontList = await _fontService.GetAsync();

                return PartialView("_Edit", model);
            }

            await _themeService.UpsertAsync(model.ThemeId, model.ThemeName, model.TitleFontId, model.TextFontId, model.LargeTitleFontSize, model.MediumTitleFontSize, model.SmallTitleFontSize, model.TinyTitleFontSize, model.TextStandardFontSize, model.PageBackgroundColour, model.MenuBackgroundColour, model.MenuTextColour);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public ActionResult Default(int themeId)
        {
            return View("_Default", new DefaultViewModel { ThemeId = themeId });
        }

        [HttpPost, ValidateAntiForgeryToken, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> Default(DefaultViewModel model)
        {
            await _themeService.DefaultAsync(model.ThemeId);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> Delete(int themeId)
        {
            await _themeService.DeleteAsync(themeId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> AppDrawer()
        {
            var model = new AppDrawerViewModel
            {
                Themes = await _themeService.GetAsync(),
                Fonts = new List<Font>()
            };

            model.Fonts.AddRange(model.Themes.Select(x => x.TextFont));
            model.Fonts.AddRange(model.Themes.Select(x => x.TitleFont));

            return View("_AppDrawer", model);
        }
    }
}