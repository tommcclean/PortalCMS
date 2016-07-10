using Portal.CMS.Entities;
using System;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface ILoginService
    {
        int? Login(string emailAddress, string password);
    }

    public class LoginService : ILoginService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public LoginService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public int? Login(string emailAddress, string password)
        {
            var userAccount = _context.Users.FirstOrDefault(x => x.EmailAddress == emailAddress);

            if (userAccount == null)
                return null;

            if (!userAccount.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
                return null;

            return userAccount.UserId;
        }
    }
}