using Portal.CMS.Services.Menu;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Menu;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class MenuController : Controller
    {
        #region Dependencies

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Settings");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel()
            {
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _menuService.Create(model.MenuName);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int menuId)
        {
            var menu = _menuService.Get(menuId);

            var model = new EditViewModel()
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                MenuItems = menu.MenuItems.ToList()
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Edit", model);

            _menuService.Edit(model.MenuId, model.MenuName);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int menuId)
        {
            _menuService.Delete(menuId);

            return RedirectToAction("Index", "Settings");
        }
    }
}