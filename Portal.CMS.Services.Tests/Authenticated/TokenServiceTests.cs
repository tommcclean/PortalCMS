using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Services.Authentication;
using System;
using System.Collections.Generic;

namespace Portal.CMS.Services.Tests.Authenticated
{
    [TestClass]
    public class TokenServiceTests
    {
        #region Dependencies

        private PortalEntityModel _mockContext;

        private UserService _userService;
        private RegistrationService _registrationService;
        private TokenService _tokenService;

        [TestInitialize]
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

        [TestMethod]
        public void RedeemPasswordToken_InvalidTokenReturnsValidation()
        {
            var result = _tokenService.RedeemPasswordToken(string.Empty, "test@test.com", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. Please Request Reset Password Token Again...");
        }

        [TestMethod]
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

            var result = _tokenService.RedeemPasswordToken("12345", "test@test.com", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. This Token does not match the Email Address you entered...");
        }

        [TestMethod]
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

            var result = _tokenService.RedeemPasswordToken("12345", "email", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. This Token has already been used");
        }

        [TestMethod]
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

            var result = _tokenService.RedeemPasswordToken("12345", "email", "test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, string.Empty);
        }

        #endregion TokenService.RedeemPasswordToken

        #region TokenService.RedeemSSOToken

        [TestMethod]
        public void RedeemSSOToken_InvalidTokenReturnsValidation()
        {
            var result = _tokenService.RedeemSSOToken(0, "12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token.");
        }

        [TestMethod]
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

            var result = _tokenService.RedeemSSOToken(userId, "12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Invalid Token. This Token has already been used.");
        }

        [TestMethod]
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

            var result = _tokenService.RedeemSSOToken(userId, "12345");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, string.Empty);
        }

        #endregion TokenService.RedeemSSOToken
    }
}