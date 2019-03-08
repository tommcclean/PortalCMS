using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Entities.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using PortalCMS.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PortalCMS.Web.Architecture.Helpers
{
	public class UserHelper
	{
		const string ADMIN_ROLE = "Admin";
		const string EDITOR_ROLE = "Editor";
	
		private static ApplicationUser CurrentUser
		{
			get
			{
				var store = new UserStore<ApplicationUser>(new PortalDbContext());
				var userManager = new UserManager<ApplicationUser>(store);
				var userId = HttpContext.Current.User.Identity.GetUserId();
				return userManager.FindById(userId);
			}
		}

		public static bool IsLoggedIn
		{
			get
			{
				return HttpContext.Current.User.Identity.IsAuthenticated;
			}
		}

		public static bool IsAdmin
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return HttpContext.Current.User.IsInRole(ADMIN_ROLE);
				}

				return false;
			}
		}

		public static bool IsEditor
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return HttpContext.Current.User.IsInRole(ADMIN_ROLE) || HttpContext.Current.User.IsInRole(EDITOR_ROLE);
				}

				return false;
			}
		}

		public static string Id
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return CurrentUser.Id;
				}

				return string.Empty;
			}
		}

		public static byte[] AvatarImage
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{ 
					return CurrentUser.AvatarImage.FileContent;
				}

				return null;
			}
		}

		public static string FullName
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return CurrentUser.FullName;
				}

				return string.Empty;
			}
		}

		public static string GivenName
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return CurrentUser.GivenName;
				}

				return string.Empty;
			}
		}

		public static string FamilyName
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return CurrentUser.FamilyName;
				}

				return string.Empty;
			}
		}

		public static string Email
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return CurrentUser.Email;
				}

				return string.Empty;
			}
		}

		public static string Bio
		{
			get
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return CurrentUser.Bio;
				}

				return "You haven't written a Bio yet...";
			}
		}
	}
}