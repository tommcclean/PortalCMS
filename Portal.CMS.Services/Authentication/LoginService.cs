using Portal.CMS.Entities;
using Portal.CMS.Services.Shared;
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

        #endregion Dependencies

        public int? Login(string emailAddress, string password)
        {
            var userAccount = _context.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress, System.StringComparison.OrdinalIgnoreCase));

            if (userAccount == null)
                return null;

            if (!SecurityHelper.CompareSecurePassword(password, userAccount.Password))
                return null;

            return userAccount.UserId;
        }
    }
}