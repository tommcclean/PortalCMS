using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Portal.CMS.Entities.Entities.Models;

namespace Portal.CMS.Entities.Seed
{
	public static class RoleSeed
	{
		public static void Seed(PortalDbContext context)
		{
			var RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

			CreateRole(RoleManager, "Admin", true);
			CreateRole(RoleManager, "Editor", true);
			CreateRole(RoleManager, "Authenticated", false);
			CreateRole(RoleManager, "Anonymous", false);
		}

		private static void CreateRole(ApplicationRoleManager roleManager, string roleName, bool IsAssignable)
		{
			var role = roleManager.FindByName(roleName);
			if (role == null)
			{
				role = new ApplicationRole(roleName);
				role.IsAssignable = IsAssignable;
				roleManager.Create(role);
			}
		}
	}
}