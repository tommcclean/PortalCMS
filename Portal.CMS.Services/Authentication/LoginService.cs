using Portal.CMS.Entities;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Portal.CMS.Services.Authentication
{
    public interface ILoginService
    {
        int? Login(string emailAddress, string password);
    }

    public class LoginService : ILoginService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public LoginService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public int? Login(string emailAddress, string password)
        {
            var userAccount = _context.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress, System.StringComparison.OrdinalIgnoreCase));

            if (userAccount == null)
                return null;

            if (!CompareSecurePassword(password, userAccount.Password))
                return null;

            return userAccount.UserId;
        }

        private static bool CompareSecurePassword(string passwordAttempt, string passwordActual)
        {
            var savedPasswordHash = passwordActual;
            /* Extract the bytes */
            var hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordAttempt, salt, 10000))
            {
                var hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        return false;

                return true;
            }
        }
    }
}