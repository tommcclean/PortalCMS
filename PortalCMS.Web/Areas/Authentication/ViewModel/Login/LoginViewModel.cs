using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Authentication.ViewModels.Login
{
	public class LoginViewModel
	{
		[Required]
		[DisplayName("Email")]
		public string EmailAddress { get; set; }

		[Required]
		[DisplayName("Password")]
		public string Password { get; set; }

		[DisplayName("Remember Me")]
		public bool RememberMe { get; set; }
	}
}