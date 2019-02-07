using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
	public static class RoleSeed
	{
		public static void Seed(PortalDbContext context)
		{
			var RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
			var roleCount = RoleManager.Roles.Count();

			CreateRole(RoleManager, "Admin");
			CreateRole(RoleManager, "Editor");
			CreateRole(RoleManager, "Authenticated");
			CreateRole(RoleManager, "Anonymous");
			context.SaveChanges();
		}

		private static void CreateRole(ApplicationRoleManager roleManager, string roleName)
		{
			var role = roleManager.FindByName(roleName);
			if (role == null)
			{
				role = new ApplicationRole(roleName);
				roleManager.Create(role);
			}
		}
	}
}