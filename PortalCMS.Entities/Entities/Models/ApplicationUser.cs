using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PortalCMS.Entities.Enumerators;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalCMS.Entities.Entities.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		[Display(Name = "Given Name")]
		public string GivenName { get; set; }

		[Required]
		[Display(Name = "Family Name")]
		public string FamilyName { get; set; }

		/// <summary>
		/// Gets users full name.
		/// </summary>
		[NotMapped]
		[Display(Name = "Full Name")]
		public virtual string FullName
		{
			get { return string.Format("{0} {1}", GivenName, FamilyName); }
		}

		/// <summary>
		/// True if the email is confirmed, default is false
		/// </summary>
		[Display(Name = "Email Confirmed")]
		public override bool EmailConfirmed { get; set; }

		/// <summary>
		/// Gets or sets the users phone number
		/// </summary>
		[Display(Name = "Phone Number")]
		public override string PhoneNumber { get; set; }

		/// <summary>
		/// True if the phone number is confirmed, default is false
		/// </summary>
		[Display(Name = "Phone Confirmed")]
		public override bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		/// Is lockout enabled for this user
		/// </summary>
		[Display(Name = "Lockout Enabled")]
		public override bool LockoutEnabled { get; set; }

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
		public virtual DateTime? LastLoginDate { get; set; }

		/// <summary>
		/// Gets or sets the users last login date and time.
		/// </summary>
		[Display(Name = "Last Logout")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm}")]
		public virtual DateTime? LastLogoutDate { get; set; }

		[Display(Name = "Short bio")]
		public string Bio { get; set; }

		[Required]
		[Display(Name = "Last Updated")]
		public DateTime LastUpdatedDate { get; set; }

		public FileDetail AvatarImage { get; set; }

		[Display(Name = "Status")]
		public virtual RecordStatus Status { get; set; }

		public ApplicationUser()
		{
			this.AvatarImage = new FileDetail();
			this.Status = RecordStatus.Enabled;
		}

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