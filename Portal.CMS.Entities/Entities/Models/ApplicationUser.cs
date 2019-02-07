using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.CMS.Entities.Entities.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
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

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in
			// CookieAuthenticationOptions.AuthenticationType 
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

			// Add custom user claims here 
			return userIdentity;
		}
	}
}