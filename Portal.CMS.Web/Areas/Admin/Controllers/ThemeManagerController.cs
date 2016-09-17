using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter]
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
        public ActionResult Delete(int themeId)
        {
            _themeService.Delete(themeId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Default(int themeId)
        {
            _themeService.Default(themeId);

            return RedirectToAction(nameof(Index));
        }
    }
}