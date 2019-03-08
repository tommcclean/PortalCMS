﻿using PortalCMS.Entities;
using PortalCMS.Entities.Entities;
using PortalCMS.Services.Authentication;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PortalCMS.Services.Menu
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuSystem>> GetAsync();

        Task<MenuSystem> GetAsync(int menuId);

        Task<List<MenuItem>> ViewAsync(string userId, string menuName);
    }

    public class MenuService : IMenuService
    {
        #region Dependencies

        readonly PortalDbContext _context;
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public MenuService(PortalDbContext context, IUserService userService, IRoleService roleService)
        {
            _context = context;
            _userService = userService;
            _roleService = roleService;
        }

        #endregion Dependencies

        public async Task<IEnumerable<MenuSystem>> GetAsync()
        {
            var results = await _context.Menus.OrderBy(x => x.MenuName).ThenBy(x => x.MenuId).ToListAsync();

            return results;
        }

        public async Task<MenuSystem> GetAsync(int menuId)
        {
            var result = await _context.Menus.FirstOrDefaultAsync(x => x.MenuId == menuId);

            return result;
        }

        public async Task<List<MenuItem>> ViewAsync(string userId, string menuName)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.MenuName == menuName);

            var menuItemList = new List<MenuItem>();

            var userRoleList = await _roleService.GetByUserAsync(userId);

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