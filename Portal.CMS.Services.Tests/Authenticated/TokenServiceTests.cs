using NUnit.Framework;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using Portal.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Tests.Authenticated
{
    [TestFixture]
    public class TokenServiceTests
    {
        #region Dependencies

        private PortalEntityModel _mockContext;

        private UserService _userService;
        private RegistrationService _registrationService;
        private TokenService _tokenService;

        [SetUp]
        public void Initialise()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _mockContext = new PortalEntityModel(connection);
            _mockContext.Database.CreateIfNotExists();

            _userService = new UserService(_mockContext);
            _registrationService = new RegistrationService(_mockContext);
            _tokenService = new TokenService(_mockContext, _userService, _registrationService);
        }

        #endregion Dependencies

        #region TokenService.RedeemPasswordToken

        [Test]
        public async Task RedeemPasswordToken_InvalidTokenReturnsValidation()
        {
            var result = await _tokenService.RedeemPasswordTokenAsync(string.Empty, "test@test.com", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. Please Request Reset Password Token Again...");
        }

        [Test]
        public void RedeemPasswordToken_TokenDoesntMatchEmailAddressReturnsValidation()
        {
            var userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.UserTokens.AddRange(new List<UserToken>
            {
                new UserToken { UserTokenId = 1, Token = "12345", DateAdded = DateTime.Now, DateRedeemed = DateTime.Now, UserTokenType = UserTokenType.ForgottenPassword, UserId = userId }
            });

            _mockContext.SaveChanges();

            var result = _tokenService.RedeemPasswordTokenAsync("12345", "test@test.com", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. This Token does not match the Email Address you entered...");
        }

        [Test]
        public void RedeemPasswordToken_TokenAlreadyUsedReturnsValidation()
        {
            var userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.UserTokens.AddRange(new List<UserToken>
            {
                new UserToken { UserTokenId = 1, Token = "12345", DateAdded = DateTime.Now, DateRedeemed = DateTime.Now, UserTokenType = UserTokenType.ForgottenPassword, UserId = userId }
            });

            _mockContext.SaveChanges();

            var result = _tokenService.RedeemPasswordTokenAsync("12345", "email", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. This Token has already been used");
        }

        [Test]
        public void RedeemPasswordToken_ExecutesWithoutException()
        {
            var userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.UserTokens.AddRange(new List<UserToken>
            {
                new UserToken { UserTokenId = 1, Token = "12345", DateAdded = DateTime.Now, UserTokenType = UserTokenType.ForgottenPassword, UserId = userId }
            });

            _mockContext.SaveChanges();

            var result = _tokenService.RedeemPasswordTokenAsync("12345", "email", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, string.Empty);
        }

        #endregion TokenService.RedeemPasswordToken

        #region TokenService.RedeemSSOToken

        [Test]
        public void RedeemSSOToken_InvalidTokenReturnsValidation()
        {
            var result = _tokenService.RedeemSSOTokenAsync(0, "12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token.");
        }

        [Test]
        public void RedeemSSOToken_TokenAlreadyUsedReturnsValidation()
        {
            var userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.UserTokens.AddRange(new List<UserToken>
            {
                new UserToken { UserTokenId = 1, Token = "12345", DateAdded = DateTime.Now, DateRedeemed = DateTime.Now, UserTokenType = UserTokenType.SSO, UserId = userId }
            });

            _mockContext.SaveChanges();

            var result = _tokenService.RedeemSSOTokenAsync(userId, "12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. This Token has already been used.");
        }

        [Test]
        public void RedeemSSOToken_ExecutesWithoutException()
        {
            var userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.UserTokens.AddRange(new List<UserToken>
            {
                new UserToken { UserTokenId = 1, Token = "12345", DateAdded = DateTime.Now, UserTokenType = UserTokenType.ForgottenPassword, UserId = userId }
            });

            _mockContext.SaveChanges();

            var result = _tokenService.RedeemSSOTokenAsync(userId, "12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, string.Empty);
        }

        #endregion TokenService.RedeemSSOToken
    }
}