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

        readonly PortalDbContext _context;
        readonly IUserService _userService;

        public RoleService(PortalDbContext context, IUserService userService)
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

            return new List<Role> { new Role { Name = "Anonymous" } };
        }

        public async Task<List<Role>> GetAsync()
        {
            var results = await _context.Roles.OrderBy(x => x.Name).ToListAsync();

            return results;
        }

        public async Task<List<Role>> GetUserAssignableRolesAsync()
        {
            var results = await _context.Roles.Where(x => x.IsAssignable).OrderBy(x => x.Name).ToListAsync();

            return results;
        }

        public async Task<Role> GetAsync(int roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == roleId);

            return role;
        }

        public async Task<int> AddAsync(string roleName)
        {
            var newRole = new Role
            {
                Name = roleName
            };

            _context.Roles.Add(newRole);

            await _context.SaveChangesAsync();

            return newRole.Id;
        }

        public async Task EditAsync(int roleId, string roleName)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == roleId);

            role.Name = roleName;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == roleId);
            if (role == null) return;

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int userId, List<string> roleList)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
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
                var matchedRole = systemRoles.FirstOrDefault(x => x.Name.Equals(role, System.StringComparison.OrdinalIgnoreCase));

                if (matchedRole == null)
                    continue;

                var userRole = new UserRole
                {
                    RoleId = matchedRole.Id,
                    UserId = user.Id
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
            if (userRoles.Any(x => x.Name.Equals("Admin", System.StringComparison.OrdinalIgnoreCase)))
                return true;

            // EVALUATE: Every Role on the Entity. One match grants access.
            foreach (var role in entityRoles)
                if (userRoles.Select(x => x.Name).Contains(role.Name))
                    return true;

            return false;
        }
    }
}