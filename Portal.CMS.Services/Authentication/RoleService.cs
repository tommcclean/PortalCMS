using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface IRoleService
    {
        IEnumerable<Role> Get(int? userId);

        List<Role> Get();

        Role Get(int roleId);

        int Add(string roleName);

        void Edit(int roleId, string roleName);

        void Update(int userId, List<string> roleList);

        void Delete(int roleId);

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

        public IEnumerable<Role> Get(int? userId)
        {
            if (userId.HasValue)
            {
                var userRoles = _context.UserRoles.Where(x => x.UserId == userId.Value).Select(x => x.Role).ToList();

                return userRoles;
            }

            return new List<Role> { new Role { RoleName = "Anonymous" } };
        }

        public List<Role> Get()
        {
            var results = _context.Roles.OrderBy(x => x.RoleName).ToList();

            return results;
        }

        public Role Get(int roleId)
        {
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == roleId);

            return role;
        }

        public int Add(string roleName)
        {
            var newRole = new Role
            {
                RoleName = roleName
            };

            _context.Roles.Add(newRole);

            _context.SaveChanges();

            return newRole.RoleId;
        }

        public void Edit(int roleId, string roleName)
        {
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == roleId);

            role.RoleName = roleName;

            _context.SaveChanges();
        }

        public void Delete(int roleId)
        {
            var role = _context.Roles.SingleOrDefault(x => x.RoleId == roleId);

            if (role == null) return;

            _context.Roles.Remove(role);

            _context.SaveChanges();
        }

        public void Update(int userId, List<string> roleList)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);

            if (user == null) return;

            var systemRoles = Get();

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

            _context.SaveChanges();
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