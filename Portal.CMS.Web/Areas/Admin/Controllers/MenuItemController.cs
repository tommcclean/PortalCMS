using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Menu;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.MenuItem;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class MenuItemController : Controller
    {
        #region Dependencies

        readonly IMenuService _menuService;
        readonly IMenuItemService _menuItemService;
        readonly IPageService _pageService;
        readonly IPostService _postService;
        readonly IRoleService _roleService;

        public MenuItemController(IMenuService menuService, IMenuItemService menuItemService, IPageService pageService, IPostService postService, IRoleService roleService)
        {
            _menuService = menuService;
            _menuItemService = menuItemService;
            _pageService = pageService;
            _postService = postService;
            _roleService = roleService;
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
                MenuList = _menuService.Get(),
                PageList = _pageService.Get(),
                PostList = _postService.Get(string.Empty, true),
                RoleList = _roleService.Get()
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
                model.PageList = _pageService.Get();
                model.PostList = _postService.Get(string.Empty, true);
                model.RoleList = _roleService.Get();

                return View("_Create", model);
            }

            var menuItemId = _menuItemService.Create(model.MenuId, model.LinkText, model.LinkURL, model.LinkIcon);

            _menuItemService.Roles(menuItemId, model.SelectedRoleList);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int menuItemId)
        {
            var menuItem = _menuItemService.Get(menuItemId);

            var model = new EditViewModel
            {
                MenuItemId = menuItem.MenuItemId,
                LinkText = menuItem.LinkText,
                LinkIcon = menuItem.LinkIcon,
                LinkURL = menuItem.LinkURL,
                RoleList = _roleService.Get(),
                SelectedRoleList = menuItem.MenuItemRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleList = _roleService.Get();

                return View(model);
            }

            _menuItemService.Edit(model.MenuItemId, model.LinkText, model.LinkURL, model.LinkIcon);

            _menuItemService.Roles(model.MenuItemId, model.SelectedRoleList);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int menuItemId)
        {
            _menuItemService.Delete(menuItemId);

            return Redirect(HttpContext.Request.UrlReferrer.ToString());
        }
    }
}