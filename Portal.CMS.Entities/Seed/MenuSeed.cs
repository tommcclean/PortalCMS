using Portal.CMS.Entities.Entities.Menu;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class MenuSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Menus.Any(x => x.MenuName == "Main Menu"))
            {
                var menuItems = new List<MenuItem>();

                menuItems.Add(new MenuItem { LinkText = "<span class=\"fa fa-home\"></span>Home", LinkAction = "Index", LinkController = "Home" });
                menuItems.Add(new MenuItem { LinkText = "<span class=\"fa fa-rss\"></span>Blog", LinkAction = "Index", LinkController = "Blog" });
                menuItems.Add(new MenuItem { LinkText = "<span class=\"fa fa-paper-plane\"></span>Contact", LinkAction = "Index", LinkController = "Contact" });

                context.Menus.Add(new Entities.Menu.Menu { MenuName = "Main Menu", MenuItems = menuItems });
            }
        }
    }
}