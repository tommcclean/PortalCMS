using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Services.Shared;
using System;
using System.Linq;

namespace Portal.CMS.Services.Authentication
{
    public interface IRegistrationService
    {
        int? Register(string emailAddress, string password, string givenName, string familyName);
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
                Password = SecurityHelper.GenerateSecurePassword(password),
                GivenName = givenName,
                FamilyName = familyName,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            _context.Users.Add(userAccount);

            _context.SaveChanges();

            return userAccount.UserId;
        }
    }
}