using Portal.CMS.Entities.Entities.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class UserHelper
    {
        public static bool IsLoggedIn
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserAccount"] == null)
                    return false;

                return true;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                var userSession = (User)System.Web.HttpContext.Current.Session["UserAccount"];
                var userRoles = (List<UserRole>)System.Web.HttpContext.Current.Session["UserRoles"];

                if (userSession == null || userRoles == null)
                    return false;

                if (userRoles.Any(x => x.Role.RoleName.Equals("Admin", System.StringComparison.OrdinalIgnoreCase)))
                    return true;

                return false;
            }
        }

        public static bool IsEditor
        {
            get
            {
                var userSession = (User)System.Web.HttpContext.Current.Session["UserAccount"];
                var userRoles = (List<UserRole>)System.Web.HttpContext.Current.Session["UserRoles"];

                if (userSession == null || userRoles == null)
                    return false;

                if (userRoles.Any(x => x.Role.RoleName == "Editor") || userRoles.Any(x => x.Role.RoleName == "Admin"))
                    return true;

                return false;
            }
        }

        public static int? UserId
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session["UserAccount"];

                if (userAccount == null)
                    return null;

                return userAccount.UserId;
            }
        }

        public static string AvatarImagePath
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session["UserAccount"];

                return userAccount.AvatarImagePath;
            }
        }

        public static string FullName
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session["UserAccount"];

                return string.Format("{0} {1}", userAccount.GivenName, userAccount.FamilyName);
            }
        }

        public static string GivenName
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session["UserAccount"];

                return userAccount.GivenName;
            }
        }

        public static string FamilyName
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session["UserAccount"];

                return userAccount.FamilyName;
            }
        }

        public static string EmailAddress
        {
            get
            {
                var userAccount = (User)System.Web.HttpContext.Current.Session["UserAccount"];

                return userAccount.EmailAddress;
            }
        }
    }
}