using Portal.CMS.Entities;
using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
    public interface ILoginService
    {
        Task<int?> LoginAsync(string emailAddress, string password);

        Task<int?> SSOAsync(int userId, string token);
    }

    public class LoginService : ILoginService
    {
        #region Dependencies

        readonly PortalEntityModel _context;
        readonly ITokenService _tokenService;

        public LoginService(PortalEntityModel context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        #endregion Dependencies

        public async Task<int?> LoginAsync(string emailAddress, string password)
        {
            var userAccount = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
            if (userAccount == null) return null;

            if (!CompareSecurePassword(password, userAccount.Password))
                return null;

            return userAccount.UserId;
        }

        public async Task<int?> SSOAsync(int userId, string token)
        {
            var tokenResult = await _tokenService.RedeemSSOTokenAsync(userId, token);

            if (string.IsNullOrWhiteSpace(tokenResult))
                return userId;

            return null;
        }

        private static bool CompareSecurePassword(string passwordAttempt, string passwordActual)
        {
            var savedPasswordHash = passwordActual;
            var hashBytes = Convert.FromBase64String(savedPasswordHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordAttempt, salt, 10000))
            {
                var hash = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        return false;
                return true;
            }
        }
    }
}