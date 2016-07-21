using Portal.CMS.Entities;
using System.Linq;

namespace Portal.CMS.Services.Menu
{
    public interface IMenuItemService
    {
        int Create(int menuId, string linkText, string linkAction, string linkController, string linkArea);

        void Edit(int menuItemId, string linkText, string linkAction, string linkController, string linkArea);

        void Delete(int menuItemId);
    }

    public class MenuItemService : IMenuItemService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public MenuItemService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public int Create(int menuId, string linkText, string linkAction, string linkController, string linkArea)
        {
            var newMenuItem = new Entities.Entities.Menu.MenuItem
            {
                MenuId = menuId,
                LinkText = linkText,
                LinkAction = linkAction,
                LinkController = linkController,
                LinkArea = linkArea
            };

            _context.MenuItems.Add(newMenuItem);

            _context.SaveChanges();

            return newMenuItem.MenuItemId;
        }

        public void Edit(int menuItemId, string linkText, string linkAction, string linkController, string linkArea)
        {
            var menuItem = _context.MenuItems.FirstOrDefault(x => x.MenuItemId == menuItemId);

            if (menuItem == null)
                return;

            menuItem.LinkText = linkText;
            menuItem.LinkAction = linkAction;
            menuItem.LinkController = linkController;
            menuItem.LinkArea = linkArea;

            _context.SaveChanges();
        }

        public void Delete(int menuItemId)
        {
            var menuItem = _context.MenuItems.FirstOrDefault(x => x.MenuItemId == menuItemId);

            if (menuItem == null)
                return;

            _context.MenuItems.Remove(menuItem);

            _context.SaveChanges();
        }
    }
}