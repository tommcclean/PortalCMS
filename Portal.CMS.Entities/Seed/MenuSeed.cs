using Portal.CMS.Entities.Entities;
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

                menuItems.Add(new MenuItem { LinkText = "Home", LinkURL = "/Home/Index", LinkIcon = "fa-home" });
                menuItems.Add(new MenuItem { LinkText = "Blog", LinkURL = "/Blog/Index", LinkIcon = "fa-book" });
                menuItems.Add(new MenuItem { LinkText = "Contact", LinkURL = "/Contact/Index", LinkIcon = "fa-envelope" });

                context.Menus.Add(new MenuSystem { MenuName = "Main Menu", MenuItems = menuItems });
            }
        }
    }
}