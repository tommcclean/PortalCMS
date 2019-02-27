using PortalCMS.Library;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Authentication.ViewModels.Registration
{
	public class RegisterViewModel
	{
		[Required]
		[DisplayName("Email")]
		public string EmailAddress { get; set; }

		[Required]
		[DisplayName("Password")]
		public string Password { get; set; }

		[Required]
		[DisplayName("First Name")]
		public string GivenName { get; set; }

		[DisplayName("Last Name")]
		public string FamilyName { get; set; }
	}
}