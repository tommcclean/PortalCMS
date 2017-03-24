using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface IUserService
    {
        User GetUser(int userId);

        IEnumerable<User> Get();

        User Get(string emailAddress);

        IEnumerable<User> GetEditors();

        IEnumerable<User> Get(List<string> roleNames);

        void UpdateDetails(int userId, string emailAddress, string givenName, string familyName);

        void UpdateAvatar(int userId, string avatarImagePath);

        void UpdateBio(int userId, string bio);

        void DeleteUser(int userId);

        int GetUserCount();
    }

    public class UserService : IUserService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public UserService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public User GetUser(int userId)
        {
            var result = _context.Users.SingleOrDefault(x => x.UserId == userId);

            return result;
        }

        public User Get(string emailAddress)
        {
            var result = _context.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress, System.StringComparison.OrdinalIgnoreCase));

            return result;
        }

        public IEnumerable<User> Get()
        {
            var userList = _context.Users.OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId);

            return userList;
        }

        public IEnumerable<User> GetEditors()
        {
            var results = _context.Users.Where(u => u.Roles.Any(r => r.Role.RoleName == "Editor") || u.Roles.Any(r => r.Role.RoleName == "Admin")).ToList();

            return results.OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName);
        }

        public IEnumerable<User> Get(List<string> roleNames)
        {
            var results = new List<User>();

            foreach (var user in _context.Users)
            {
                foreach (var roleName in roleNames)
                {
                    if (user.Roles.Any(x => x.Role.RoleName == roleName))
                        results.Add(user);
                }
            }

            return results.Distinct().OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId);
        }

        public int GetUserCount()
        {
            return _context.Users.Count();
        }

        public void UpdateDetails(int userId, string emailAddress, string givenName, string familyName)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);

            user.EmailAddress = emailAddress;
            user.GivenName = givenName;
            user.FamilyName = familyName;

            _context.SaveChanges();
        }

        public void UpdateAvatar(int userId, string avatarImagePath)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);

            user.AvatarImagePath = avatarImagePath;

            _context.SaveChanges();
        }

        public void UpdateBio(int userId, string bio)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);

            user.Bio = bio;

            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);

            _context.Users.Remove(user);

            _context.SaveChanges();
        }
    }
}