﻿using PortalCMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PortalCMS.Entities.Seed
{
    public static class MenuSeed
    {
        public static void Seed(PortalDbContext context)
        {
            if (!context.Menus.Any(x => x.MenuName == "Main Menu"))
            {
                var menuItems = new List<MenuItem>();

                menuItems.Add(new MenuItem { LinkText = "Home", LinkURL = "/Home/Index", LinkIcon = "fa-home" });
                menuItems.Add(new MenuItem { LinkText = "Blog", LinkURL = "/Blog/Read/Index", LinkIcon = "fa-book" });

                context.Menus.Add(new MenuSystem { MenuName = "Main Menu", MenuItems = menuItems });

                context.SaveChanges();
            }
        }
    }
}