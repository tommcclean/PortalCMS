using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.UserManager
{
	public class DetailsViewModel
	{
		public string UserId { get; set; }

		[Required]
		[DisplayName("Email")]
		public string EmailAddress { get; set; }

		[Required]
		[DisplayName("First Name")]
		public string GivenName { get; set; }

		[Required]
		[DisplayName("Last Name")]
		public string FamilyName { get; set; }

		[DisplayName("Registered")]
		public DateTime DateAdded { get; set; }

		public DateTime DateUpdated { get; set; }
	}
}