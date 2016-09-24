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

                menuItems.Add(new MenuItem { LinkText = "<span class=\"fa fa-home\"></span>Home", LinkURL = "/Home/Index", LinkIcon = "fa-home" });
                menuItems.Add(new MenuItem { LinkText = "<span class=\"fa fa-rss\"></span>Blog", LinkURL = "/Blog/Index", LinkIcon = "fa-book" });
                menuItems.Add(new MenuItem { LinkText = "<span class=\"fa fa-paper-plane\"></span>Contact", LinkURL = "/Contact/Index", LinkIcon = "fa-envelope" });

                context.Menus.Add(new Entities.Menu.Menu { MenuName = "Main Menu", MenuItems = menuItems });
            }
        }
    }
}