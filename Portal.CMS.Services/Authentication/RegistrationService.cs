using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
    public interface IRegistrationService
    {
        Task<int?> RegisterAsync(string emailAddress, string password, string givenName, string familyName);

        Task ChangePasswordAsync(int userId, string newPassword);
    }

    public class RegistrationService : IRegistrationService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public RegistrationService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task<int?> RegisterAsync(string emailAddress, string password, string givenName, string familyName)
        {
            if (await _context.Users.AnyAsync(x => x.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase)))
                return -1;

            var userAccount = new User
            {
                EmailAddress = emailAddress,
                Password = GenerateSecurePassword(password),
                GivenName = givenName,
                FamilyName = familyName,
                AvatarImagePath = "/Areas/Admin/Content/Images/profile-image-male.png",
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.Users.Add(userAccount);

            await _context.SaveChangesAsync();

            return userAccount.UserId;
        }

        public async Task ChangePasswordAsync(int userId, string newPassword)
        {
            var userAccount = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            if (userAccount == null) return;

            userAccount.Password = GenerateSecurePassword(newPassword);
            userAccount.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        private static string GenerateSecurePassword(string password)
        {
            // http://stackoverflow.com/questions/4181198/how-to-hash-a-password
            using (var rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var salt = new byte[16];
                rNGCryptoServiceProvider.GetBytes(salt);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                {
                    var hash = pbkdf2.GetBytes(20);
                    var hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    var savedPasswordHash = Convert.ToBase64String(hashBytes);
                    return savedPasswordHash;
                }
            }
        }
    }
}