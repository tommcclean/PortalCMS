using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Authentication;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Portal.CMS.Services.Authentication
{
    public interface IRegistrationService
    {
        int? Register(string emailAddress, string password, string givenName, string familyName);

        void ChangePassword(int userId, string newPassword);
    }

    public class RegistrationService : IRegistrationService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        #endregion Dependencies

        public int? Register(string emailAddress, string password, string givenName, string familyName)
        {
            if (_context.Users.Any(x => x.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase)))
                return -1;

            var userAccount = new User()
            {
                EmailAddress = emailAddress,
                Password = GenerateSecurePassword(password),
                GivenName = givenName,
                FamilyName = familyName,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            _context.Users.Add(userAccount);

            _context.SaveChanges();

            return userAccount.UserId;
        }

        public void ChangePassword(int userId, string newPassword)
        {
            var userAccount = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (userAccount == null)
                return;

            userAccount.Password = GenerateSecurePassword(newPassword);
            userAccount.DateUpdated = DateTime.Now;

            _context.SaveChanges();
        }

        private string GenerateSecurePassword(string password)
        {
            // http://stackoverflow.com/questions/4181198/how-to-hash-a-password

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
    }
}