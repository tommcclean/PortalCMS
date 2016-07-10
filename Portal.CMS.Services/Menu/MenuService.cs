using Portal.CMS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Menu
{
    public interface IMenuService
    {
        IEnumerable<Entities.Entities.Menu.Menu> Get();

        Entities.Entities.Menu.Menu Get(int menuId);

        Entities.Entities.Menu.Menu Get(string menuName);

        int Create(string menuName);

        void Edit(int menuId, string menuName);

        void Delete(int menuId);
    }

    public class MenuService : IMenuService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public MenuService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public IEnumerable<Entities.Entities.Menu.Menu> Get()
        {
            var results = _context.Menus.OrderBy(x => x.MenuName).ThenBy(x => x.MenuId);

            return results;
        }

        public Entities.Entities.Menu.Menu Get(int menuId)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.MenuId == menuId);

            return menu;
        }

        public Entities.Entities.Menu.Menu Get(string menuName)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.MenuName == menuName);

            return menu;
        }

        public int Create(string menuName)
        {
            var newMenu = new Entities.Entities.Menu.Menu()
            {
                MenuName = menuName
            };

            _context.Menus.Add(newMenu);

            _context.SaveChanges();

            return newMenu.MenuId;
        }

        public void Edit(int menuId, string menuName)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.MenuId == menuId);

            if (menu == null)
                return;

            menu.MenuName = menuName;

            _context.SaveChanges();
        }

        public void Delete(int menuId)
        {
            var menu = _context.Menus.FirstOrDefault(x => x.MenuId == menuId);

            if (menu == null)
                return;

            _context.Menus.Remove(menu);

            _context.SaveChanges();
        }
    }
}