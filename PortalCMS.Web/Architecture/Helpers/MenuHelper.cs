﻿using PortalCMS.Entities.Entities;
using PortalCMS.Services.Menu;
using PortalCMS.Web.DependencyResolution;
using System.Collections.Generic;

namespace PortalCMS.Web.Architecture.Helpers
{
	public static class MenuHelper
	{
		public static List<MenuItem> Get(string menuName)
		{
			var validSession = System.Web.HttpContext.Current.Session != null;

			object sessionMenu = null;

			if (validSession)
			{
				sessionMenu = System.Web.HttpContext.Current.Session[$"Menu-{menuName}"];
			}

			if (sessionMenu != null)
			{
				return (List<MenuItem>)sessionMenu;
			}
			else
			{
				var container = IoC.Initialize();

				IMenuService menuService = container.GetInstance<MenuService>();

				var menuItems = AsyncHelpers.RunSync(() => menuService.ViewAsync(UserHelper.Id, menuName));

				if (validSession)
				{
					System.Web.HttpContext.Current.Session.Add($"Menu-{menuName}", menuItems);
				}

				return menuItems;
			}
		}
	}
}