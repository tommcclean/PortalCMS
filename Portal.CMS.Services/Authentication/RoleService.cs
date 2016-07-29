using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Authentication;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface IRoleService
    {
        List<UserRole> Get(int userId);

        List<Role> Get();

        Role Fetch(int roleId);

        int Add(string roleName);

        void Edit(int roleId, string roleName);

        void UpdateUserRoles(int userId, List<string> roleList);

        void Delete(int roleId);
    }

    public class RoleService : IRoleService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public RoleService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public List<UserRole> Get(int userId)
        {
            var results = _context.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId).ToList();

            return results;
        }

        public List<Role> Get()
        {
            var results = _context.Roles.OrderBy(x => x.RoleName).ToList();

            return results;
        }

        public Role Fetch(int roleId)
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

            if (role == null)
                return;

            _context.Roles.Remove(role);

            _context.SaveChanges();
        }

        public void UpdateUserRoles(int userId, List<string> roleList)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);
            if (user == null) return;

            var systemRoles = Get();
            var userRoles = Get(userId);

            foreach (var role in userRoles)
                _context.UserRoles.Remove(role);

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
    }
}