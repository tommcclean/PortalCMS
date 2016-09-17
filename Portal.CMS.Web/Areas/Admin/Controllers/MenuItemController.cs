using Portal.CMS.Services.Menu;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.MenuItem;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class MenuItemController : Controller
    {
        #region Dependencies

        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuService menuService, IMenuItemService menuItemService)
        {
            _menuService = menuService;
            _menuItemService = menuItemService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "SettingManager");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel()
            {
                MenuList = _menuService.Get()
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MenuList = _menuService.Get();

                return View("_Create", model);
            }

            _menuItemService.Create(model.MenuId, model.LinkText, model.LinkAction, model.LinkController, model.LinkArea);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int menuItemId)
        {
            _menuItemService.Delete(menuItemId);

            return RedirectToAction("Index", "SettingManager");
        }
    }
}