using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Entities.Entities.Menu;
using Portal.CMS.Services.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Menu
{
    public interface IMenuService
    {
        IEnumerable<Entities.Entities.Menu.Menu> Get();

        Entities.Entities.Menu.Menu Get(int menuId);

        Entities.Entities.Menu.Menu Get(string menuName);

        List<MenuItem> View(int? userId, string menuName);

        int Create(string menuName);

        void Edit(int menuId, string menuName);

        void Delete(int menuId);
    }

    public class MenuService : IMenuService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public MenuService(PortalEntityModel context, IUserService userService, IRoleService roleService)
        {
            _context = context;
            _userService = userService;
            _roleService = roleService;
        }

        #endregion Dependencies

        public IEnumerable<Entities.Entities.Menu.Menu> Get()
        {
            var results = _context.Menus.OrderBy(x => x.MenuName).ThenBy(x => x.MenuId);

            return results;
        }

        public Entities.Entities.Menu.Menu Get(int menuId)
        {
            var menu = _context.Menus.SingleOrDefault(x => x.MenuId == menuId);

            return menu;
        }

        public Entities.Entities.Menu.Menu Get(string menuName)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.MenuName == menuName);

            return menu;
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

        public int Create(string menuName)
        {
            var newMenu = new Entities.Entities.Menu.Menu
            {
                MenuName = menuName
            };

            _context.Menus.Add(newMenu);

            _context.SaveChanges();

            return newMenu.MenuId;
        }

        public void Edit(int menuId, string menuName)
        {
            var menu = _context.Menus.SingleOrDefault(x => x.MenuId == menuId);

            if (menu == null)
                return;

            menu.MenuName = menuName;

            _context.SaveChanges();
        }

        public void Delete(int menuId)
        {
            var menu = _context.Menus.SingleOrDefault(x => x.MenuId == menuId);

            if (menu == null)
                return;

            _context.Menus.Remove(menu);

            _context.SaveChanges();
        }
    }
}