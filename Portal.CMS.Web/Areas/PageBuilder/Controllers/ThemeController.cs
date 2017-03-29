using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Areas.PageBuilder.ViewModels.Theme;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    public class ThemeController : Controller
    {
        #region Dependencies

        readonly IThemeService _themeService;

        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        #endregion Dependencies

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