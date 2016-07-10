using Portal.CMS.Services.Menu;
using Portal.CMS.Web.DependencyResolution;
using StructureMap;

namespace Portal.CMS.Web.Areas.Admin.Helpers
{
    public static class MenuHelper
    {
        public static Portal.CMS.Entities.Entities.Menu.Menu Get(string menuName)
        {
            IContainer container = IoC.Initialize();

            IMenuService menuService = container.GetInstance<MenuService>();

            var menu = menuService.Get(menuName);

            return menu;
        }
    }
}