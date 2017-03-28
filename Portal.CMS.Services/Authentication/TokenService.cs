using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using System;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface ITokenService
    {
        string Add(string emailAddress, UserTokenType userTokenType);

        string RedeemPasswordToken(string token, string emailAddress, string password);

        string RedeemSSOToken(int userid, string token);
    }

    public class TokenService : ITokenService
    {
        #region Dependencies

        readonly PortalEntityModel _context;
        readonly IUserService _userService;
        readonly IRegistrationService _registrationService;

        public TokenService(PortalEntityModel context, IUserService userService, IRegistrationService registrationService)
        {
            _context = context;
            _userService = userService;
            _registrationService = registrationService;
        }

        #endregion Dependencies

        public string Add(string emailAddress, UserTokenType userTokenType)
        {
            var user = _userService.Get(emailAddress);
            if (user == null) return string.Empty;

            var userToken = new UserToken
            {
                UserId = user.UserId,
                Token = Guid.NewGuid().ToString(),
                UserTokenType = userTokenType,
                DateAdded = DateTime.Now
            };

            _context.UserTokens.Add(userToken);

            _context.SaveChanges();

            return userToken.Token;
        }

        public string RedeemPasswordToken(string token, string emailAddress, string password)
        {
            var userToken = _context.UserTokens.FirstOrDefault(x => x.Token.Equals(token, StringComparison.OrdinalIgnoreCase));

            if (userToken == null)
                return "Invalid Token. Please Request Reset Password Token Again...";

            if (!userToken.User.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))
                return "Invalid Token. This Token does not match the Email Address you entered...";

            if (userToken.DateRedeemed.HasValue)
                return "Invalid Token. This Token has already been used";

            _registrationService.ChangePassword(userToken.User.UserId, password);

            userToken.DateRedeemed = DateTime.Now;
            _context.SaveChanges();

            return string.Empty;
        }

        public string RedeemSSOToken(int userid, string token)
        {
            var userToken = _context.UserTokens.FirstOrDefault(x => x.Token.Equals(token, StringComparison.OrdinalIgnoreCase) && x.UserId == userid);

            if (userToken == null)
                return "Invalid Token.";

            if (userToken.DateRedeemed.HasValue)
                return "Invalid Token. This Token has already been used.";

            userToken.DateRedeemed = DateTime.Now;
            _context.SaveChanges();

            return string.Empty;
        }
    }
}