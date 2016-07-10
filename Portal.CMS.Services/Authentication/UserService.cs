using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface IUserService
    {
        User GetUser(int userId);

        IEnumerable<User> Get();

        void UpdateUser(int userId, string emailAddress, string givenName, string familyName);

        void DeleteUser(int userId);

        int GetUserCount();
    }

    public class UserService : IUserService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public UserService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public User GetUser(int userId)
        {
            var result = _context.Users.FirstOrDefault(x => x.UserId == userId);

            return result;
        }

        public IEnumerable<User> Get()
        {
            var userList = _context.Users.OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId);

            return userList;
        }

        public int GetUserCount()
        {
            return _context.Users.Count();
        }

        public void UpdateUser(int userId, string emailAddress, string givenName, string familyName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            user.EmailAddress = emailAddress;
            user.GivenName = givenName;
            user.FamilyName = familyName;

            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            _context.Users.Remove(user);

            _context.SaveChanges();
        }
    }
}