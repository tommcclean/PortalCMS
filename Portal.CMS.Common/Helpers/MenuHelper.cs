﻿using Portal.CMS.Entities.Entities.Menu;
using Portal.CMS.Services.Menu;
using StructureMap;
using System.Collections.Generic;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class MenuHelper
    {
        public static List<MenuItem> Get(string menuName)
        {
            var container = SettingHelper.Initialize();

            IMenuService menuService = container.GetInstance<MenuService>();

            var menuItems = menuService.View(UserHelper.UserId, menuName);

            return menuItems;
        }
    }
}