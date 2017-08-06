using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Menu
{
    public interface IMenuItemService
    {
        Task<MenuItem> GetAsync(int menuItemId);

        Task<int> CreateAsync(int menuId, string linkText, string linkURL, string linkIcon);

        Task EditAsync(int menuItemId, string linkText, string linkURL, string linkIcon);

        Task DeleteAsync(int menuItemId);

        Task RolesAsync(int menuItemId, List<string> roleList);
    }

    public class MenuItemService : IMenuItemService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public MenuItemService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task<MenuItem> GetAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems.Include(x => x.Menu).SingleOrDefaultAsync(x => x.MenuItemId == menuItemId);

            return menuItem;
        }

        public async Task<int> CreateAsync(int menuId, string linkText, string linkURL, string linkIcon)
        {
            var newMenuItem = new MenuItem
            {
                MenuId = menuId,
                LinkText = linkText,
                LinkURL = linkURL,
                LinkIcon = linkIcon
            };

            _context.MenuItems.Add(newMenuItem);

            await _context.SaveChangesAsync();

            return newMenuItem.MenuItemId;
        }

        public async Task EditAsync(int menuItemId, string linkText, string linkURL, string linkIcon)
        {
            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(x => x.MenuItemId == menuItemId);
            if (menuItem == null) return;

            menuItem.LinkText = linkText;
            menuItem.LinkURL = linkURL;
            menuItem.LinkIcon = linkIcon;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(x => x.MenuItemId == menuItemId);
            if (menuItem == null) return;

            _context.MenuItems.Remove(menuItem);

            await _context.SaveChangesAsync();
        }

        public async Task RolesAsync(int menuItemId, List<string> roleList)
        {
            var menuItem = await GetAsync(menuItemId);
            if (menuItem == null) return;

            var roles = await _context.Roles.ToListAsync();

            if (menuItem.MenuItemRoles != null)
                foreach (var role in menuItem.MenuItemRoles.ToList())
                    _context.MenuItemRoles.Remove(role);

            foreach (var roleName in roleList)
            {
                var currentRole = roles.FirstOrDefault(x => x.RoleName == roleName);

                if (currentRole == null) continue;

                _context.MenuItemRoles.Add(new MenuItemRole { MenuItemId = menuItemId, RoleId = currentRole.RoleId });
            }

            await _context.SaveChangesAsync();
        }
    }
}