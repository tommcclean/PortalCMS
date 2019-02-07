using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Entities.Models;
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
				var userSession = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];
				var userRoles = (IEnumerable<string>)System.Web.HttpContext.Current.Session[USER_ROLES];

				if (userSession == null || userRoles == null)
					return false;

				if (userRoles.Any(x => x.Equals(ADMIN_ROLE, System.StringComparison.OrdinalIgnoreCase)))
					return true;

				return false;
			}
		}

		public static bool IsEditor
		{
			get
			{
				var userSession = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];
				var userRoles = (IEnumerable<string>)System.Web.HttpContext.Current.Session[USER_ROLES];

				if (userSession == null || userRoles == null)
					return false;

				foreach(var role in userRoles)
				{
					if (role == EDITOR_ROLE || role == ADMIN_ROLE)
					{
						return true;
					}
				}

				if (userRoles.Any(x => x.Equals(EDITOR_ROLE, System.StringComparison.OrdinalIgnoreCase)) || userRoles.Any(x => x.Equals(ADMIN_ROLE, System.StringComparison.OrdinalIgnoreCase)))
					return true;

				return false;
			}
		}

		public static string Id
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				if (userAccount == null)
					return string.Empty;

				return userAccount.Id;
			}
		}

		public static string AvatarImagePath
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				return userAccount.AvatarImagePath;
			}
		}

		public static string FullName
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				return $"{userAccount.GivenName} {userAccount.FamilyName}";
			}
		}

		public static string GivenName
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				return userAccount.GivenName;
			}
		}

		public static string FamilyName
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				return userAccount.FamilyName;
			}
		}

		public static string Email
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				return userAccount.Email;
			}
		}

		public static string Bio
		{
			get
			{
				var userAccount = (ApplicationUser)System.Web.HttpContext.Current.Session[USER_ACCOUNT];

				if (string.IsNullOrWhiteSpace(userAccount.Bio))
					return "You haven't written a Bio yet...";

				return userAccount.Bio;
			}
		}
	}
}