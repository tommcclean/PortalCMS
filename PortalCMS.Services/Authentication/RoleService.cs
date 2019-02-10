using PortalCMS.Entities;
using PortalCMS.Entities.Entities.Models;
using PortalCMS.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PortalCMS.Services.Authentication
{
	public interface IRoleService : IRepositoryBase<ApplicationRole>
	{
        Task<IEnumerable<string>> GetByUserAsync(string userId);

        Task<List<ApplicationRole>> GetAsync();

        Task<List<ApplicationRole>> GetUserAssignableRolesAsync();

        Task<string> AddAsync(string roleName);

        Task EditAsync(string roleId, string roleName);

        Task UpdateAsync(string userId, List<string> roleList);

        Task DeleteAsync(string roleId);

        bool Validate(IEnumerable<ApplicationRole> entityRoles, IEnumerable<string> userRoles);
    }

	public class RoleService : ServiceBase<ApplicationRole>, IRoleService
	{
		#region Dependencies

		readonly PortalDbContext _context;
		readonly IUserService _userService;

		public RoleService(PortalDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		#endregion Dependencies

		public async Task<IEnumerable<string>> GetByUserAsync(string userId)
		{
			if (!string.IsNullOrEmpty(userId))
			{
				var user = await UserManager.FindByIdAsync(userId);

				// get user roles
				if(user != null)
				{
					IList<string> roles = await UserManager.GetRolesAsync(userId);
					return roles;
				}
			}

			return new List<string> { "Anonymous"  };
		}

		public async Task<List<ApplicationRole>> GetAsync()
		{
			var results = await RoleManager.Roles.ToListAsync();

			return results;
		}

		public async Task<List<ApplicationRole>> GetUserAssignableRolesAsync()
		{
			var results = await RoleManager.Roles.Where(x => x.IsAssignable).OrderBy(x => x.Name).ToListAsync();

			return results;
		}

		public async Task<string> AddAsync(string roleName)
		{
			var newRole = new ApplicationRole
			{
				Name = roleName
			};

			_context.Roles.Add(newRole);

			await _context.SaveChangesAsync();

			return newRole.Id;
		}

		public async Task EditAsync(string roleId, string roleName)
		{
			var role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == roleId);

			role.Name = roleName;

			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string roleId)
		{
			var role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == roleId);
			if (role == null) return;

			_context.Roles.Remove(role);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(string userId, List<string> roleList)
		{
			var user = await UserManager.FindByIdAsync(userId);
			if (user == null) return;

			var roles = await UserManager.GetRolesAsync(userId);
			await UserManager.RemoveFromRolesAsync(userId, roles.ToArray());

			var systemRoles = await RoleManager.Roles.ToListAsync();

			foreach (var role in roleList)
			{
				var matchedRole = await RoleManager.FindByNameAsync(role);

				if (matchedRole == null)
					continue;

				await UserManager.AddToRoleAsync(userId, role);
			}
		}

		public bool Validate(IEnumerable<ApplicationRole> entityRoles, IEnumerable<string> userRoles)
		{
			// PASS: Where no roles are specified on the entity, access is granted to all users.
			if (!entityRoles.Any())
				return true;

			// PASS: Administrators can access any content.
			if (userRoles.Any(x => x.Equals("Admin", System.StringComparison.OrdinalIgnoreCase)))
				return true;

			// EVALUATE: Every Role on the Entity. One match grants access.
			foreach (var role in entityRoles)
				if (userRoles.Contains(role.Name))
					return true;

			return false;
		}
	}
}