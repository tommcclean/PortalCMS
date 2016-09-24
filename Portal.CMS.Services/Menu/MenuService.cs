using Portal.CMS.Entities;
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

        public MenuService(PortalEntityModel context, IUserService userService)
        {
            _context = context;
            _userService = userService;
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

            var userRoleList = new List<string>();
            var menuItemList = new List<MenuItem>();

            if (userId.HasValue)
            {
                var user = _userService.GetUser(userId.Value);

                if (user.Roles.Any(x => x.Role.RoleName == "Admin"))
                    return menu.MenuItems.ToList();

                if (user != null)
                    userRoleList.AddRange(user.Roles.Select(x => x.Role.RoleName));
            }

            foreach (var menuItem in menu.MenuItems)
            {
                if (!menuItem.MenuItemRoles.Any())
                {
                    menuItemList.Add(menuItem);

                    continue;
                }

                foreach (var role in userRoleList)
                {
                    if (menuItem.MenuItemRoles.Select(x => x.Role.RoleName).Contains(role))
                    {
                        menuItemList.Add(menuItem);

                        continue;
                    }
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