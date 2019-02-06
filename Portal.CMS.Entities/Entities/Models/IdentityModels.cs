using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.CMS.Entities.Entities.Models
{
	public class CustomUserRole : IdentityUserRole<int> {  }
	public class CustomUserClaim : IdentityUserClaim<int> { }
	public class CustomUserLogin : IdentityUserLogin<int> { }

	public class CustomRole : IdentityRole<int, CustomUserRole>
	{
		public CustomRole() { }
		public CustomRole(string name) { Name = name; }

		[Required]
		[DefaultValue("true")]
		public bool IsAssignable { get; set; }
	}

	public class CustomUserStore : UserStore<CustomUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
	{
		public CustomUserStore(PortalDbContext context): base(context)
		{
		}
	}

	public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
	{
		public CustomRoleStore(PortalDbContext context): base(context)
		{
		}
	}

	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class CustomUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
	{
		[Required]
		public string GivenName { get; set; }

		[Required]
		public string FamilyName { get; set; }

		[Required]
		public DateTime DateAdded { get; set; }

		[Required]
		public DateTime DateUpdated { get; set; }

		public string AvatarImagePath { get; set; }

		public string Bio { get; set; }

		/// <summary>
		/// Gets or sets the users registration date and time.
		/// </summary>
		[Required]
		[Display(Name = "Registered On")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm}")]
		public virtual DateTime RegistrationDate { get; set; }

		/// <summary>
		/// Gets or sets the users last login date and time.
		/// </summary>
		[Display(Name = "Last Login")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm}")]
		public virtual DateTime? LastLoginTime { get; set; }

		/// <summary>
		/// Gets or sets the users last login date and time.
		/// </summary>
		[Display(Name = "Last LogOff")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm}")]
		public virtual DateTime? LastLogoutTime { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CustomUser, int> manager)
		{
			// Note the authenticationType must match the one defined in
			// CookieAuthenticationOptions.AuthenticationType 
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

			// Add custom user claims here 
			return userIdentity;
		}
	}
}