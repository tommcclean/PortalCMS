using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Menu;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.MenuItem;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter(ActionFilterResponseType.Modal)]
    public class MenuItemController : Controller
    {
        #region Dependencies

        private readonly IMenuService _menuService;
        private readonly IMenuItemService _menuItemService;
        private readonly IPageService _pageService;
        private readonly IPostService _postService;
        private readonly IRoleService _roleService;

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
        public async Task<ActionResult> Create()
        {
            var model = new CreateViewModel
            {
                MenuList = await _menuService.GetAsync(),
                PageList = await _pageService.GetAsync(),
                PostList = await _postService.GetAsync(string.Empty, true),
                RoleList = await _roleService.GetAsync()
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MenuList = await _menuService.GetAsync();
                model.PageList = await _pageService.GetAsync();
                model.PostList = await _postService.GetAsync(string.Empty, true);
                model.RoleList = await _roleService.GetAsync();

                return View("_Create", model);
            }

            var menuItemId = await _menuItemService.CreateAsync(model.MenuId, model.LinkText, model.LinkURL, model.LinkIcon);

            await _menuItemService.RolesAsync(menuItemId, model.SelectedRoleList);

            await ResetSessionMenuAsync(model.MenuId);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int menuItemId)
        {
            var menuItem = await _menuItemService.GetAsync(menuItemId);

            var model = new EditViewModel
            {
                MenuId = menuItem.MenuId,
                MenuItemId = menuItem.MenuItemId,
                LinkText = menuItem.LinkText,
                LinkIcon = menuItem.LinkIcon,
                LinkURL = menuItem.LinkURL,
                RoleList = await _roleService.GetAsync(),
                SelectedRoleList = menuItem.MenuItemRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleList = await _roleService.GetAsync();

                return View(model);
            }

            await _menuItemService.EditAsync(model.MenuItemId, model.LinkText, model.LinkURL, model.LinkIcon);

            await _menuItemService.RolesAsync(model.MenuItemId, model.SelectedRoleList);

            await ResetSessionMenuAsync(model.MenuId);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int menuItemId)
        {
            var menuItem = await _menuItemService.GetAsync(menuItemId);
            var menuId = menuItem.MenuId;

            await _menuItemService.DeleteAsync(menuItemId);

            await ResetSessionMenuAsync(menuId);

            return Redirect(HttpContext.Request.UrlReferrer.ToString());
        }

        private async Task ResetSessionMenuAsync(int menuId)
        {
            var menu = await _menuService.GetAsync(menuId);

            System.Web.HttpContext.Current.Session.Remove($"Menu-{menu.MenuName}");
        }
    }
}