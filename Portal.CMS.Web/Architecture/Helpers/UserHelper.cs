using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class UserHelper
    {
        const string USER_ACCOUNT = "UserAccount";
        const string USER_ROLES = "UserRoles";
        const string ADMIN_ROLE = "Admin";
        const string EDITOR_ROLE = "Editor";

        public static bool IsLoggedIn
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[USER_ACCOUNT] == null)
                    return false;

                return true;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                var userSession = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];
                var userRoles = (IEnumerable<Role>)System.Web.HttpContext.Current.Session[USER_ROLES];

                if (userSession == null || userRoles == null)
                    return false;

                if (userRoles.Any(x => x.RoleName.Equals(ADMIN_ROLE, System.StringComparison.OrdinalIgnoreCase)))
                    return true;

                return false;
            }
        }

        public static bool IsEditor
        {
            get
            {
                var userSession = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];
                var userRoles = (IEnumerable<Role>)System.Web.HttpContext.Current.Session[USER_ROLES];

                if (userSession == null || userRoles == null)
                    return false;

                if (userRoles.Any(x => x.RoleName == EDITOR_ROLE) || userRoles.Any(x => x.RoleName == ADMIN_ROLE))
                    return true;

                return false;
            }
        }

        public static int? UserId
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                if (userAccount == null)
                    return null;

                return userAccount.UserId;
            }
        }

        public static string AvatarImagePath
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                return userAccount.AvatarImagePath;
            }
        }

        public static string FullName
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                return $"{userAccount.GivenName} {userAccount.FamilyName}";
            }
        }

        public static string GivenName
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                return userAccount.GivenName;
            }
        }

        public static string FamilyName
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                return userAccount.FamilyName;
            }
        }

        public static string EmailAddress
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                return userAccount.EmailAddress;
            }
        }

        public static string Bio
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

                if (string.IsNullOrWhiteSpace(userAccount.Bio))
                    return "You haven't written a Bio yet...";

                return userAccount.Bio;
            }
        }
    }
}