using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Menu
{
    public interface IMenuService
    {
        IEnumerable<MenuSystem> Get();

        List<MenuItem> View(int? userId, string menuName);
    }

    public class MenuService : IMenuService
    {
        #region Dependencies

        readonly PortalEntityModel _context;
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public MenuService(PortalEntityModel context, IUserService userService, IRoleService roleService)
        {
            _context = context;
            _userService = userService;
            _roleService = roleService;
        }

        #endregion Dependencies

        public IEnumerable<MenuSystem> Get()
        {
            var results = _context.Menus.OrderBy(x => x.MenuName).ThenBy(x => x.MenuId).ToList();

            return results;
        }

        public List<MenuItem> View(int? userId, string menuName)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.MenuName == menuName);

            var menuItemList = new List<MenuItem>();

            var userRoleList = _roleService.Get(userId);

            foreach (var menuItem in menu.MenuItems)
            {
                var hasAccess = _roleService.Validate(menuItem.MenuItemRoles.Select(x => x.Role), userRoleList);

                if (hasAccess)
                {
                    menuItemList.Add(menuItem);

                    continue;
                }
            }

            return menuItemList;
        }
    }
}