using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAsync(int? userId);

        Task<List<Role>> GetAsync();

        Task<List<Role>> GetUserAssignableRolesAsync();

        Task<Role> GetAsync(int roleId);

        Task<int> AddAsync(string roleName);

        Task EditAsync(int roleId, string roleName);

        Task UpdateAsync(int userId, List<string> roleList);

        Task DeleteAsync(int roleId);

        bool Validate(IEnumerable<Role> entityRoles, IEnumerable<Role> userRoles);
    }

    public class RoleService : IRoleService
    {
        #region Dependencies

        readonly PortalEntityModel _context;
        readonly IUserService _userService;

        public RoleService(PortalEntityModel context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        #endregion Dependencies

        public async Task<IEnumerable<Role>> GetAsync(int? userId)
        {
            if (userId.HasValue)
            {
                var userRoles = await _context.UserRoles.Where(x => x.UserId == userId.Value).Select(x => x.Role).ToListAsync();

                return userRoles;
            }

            return new List<Role> { new Role { RoleName = "Anonymous" } };
        }

        public async Task<List<Role>> GetAsync()
        {
            var results = await _context.Roles.OrderBy(x => x.RoleName).ToListAsync();

            return results;
        }

        public async Task<List<Role>> GetUserAssignableRolesAsync()
        {
            var results = await _context.Roles.Where(x => x.IsAssignable).OrderBy(x => x.RoleName).ToListAsync();

            return results;
        }

        public async Task<Role> GetAsync(int roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == roleId);

            return role;
        }

        public async Task<int> AddAsync(string roleName)
        {
            var newRole = new Role
            {
                RoleName = roleName
            };

            _context.Roles.Add(newRole);

            await _context.SaveChangesAsync();

            return newRole.RoleId;
        }

        public async Task EditAsync(int roleId, string roleName)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == roleId);

            role.RoleName = roleName;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == roleId);
            if (role == null) return;

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int userId, List<string> roleList)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return;

            var systemRoles = await GetAsync();

            if (user.Roles != null)
            {
                while (user.Roles.Any())
                {
                    var role = user.Roles.First();

                    _context.UserRoles.Remove(role);
                }
            }

            foreach (var role in roleList)
            {
                var matchedRole = systemRoles.FirstOrDefault(x => x.RoleName.Equals(role, System.StringComparison.OrdinalIgnoreCase));

                if (matchedRole == null)
                    continue;

                var userRole = new UserRole
                {
                    RoleId = matchedRole.RoleId,
                    UserId = user.UserId
                };

                _context.UserRoles.Add(userRole);
            }

            await _context.SaveChangesAsync();
        }

        public bool Validate(IEnumerable<Role> entityRoles, IEnumerable<Role> userRoles)
        {
            // PASS: Where no roles are specified on the entity, access is granted to all users.
            if (!entityRoles.Any())
                return true;

            // PASS: Administrators can access any content.
            if (userRoles.Any(x => x.RoleName.Equals("Admin", System.StringComparison.OrdinalIgnoreCase)))
                return true;

            // EVALUATE: Every Role on the Entity. One match grants access.
            foreach (var role in entityRoles)
                if (userRoles.Select(x => x.RoleName).Contains(role.RoleName))
                    return true;

            return false;
        }
    }
}