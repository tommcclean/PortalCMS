using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Authentication.ViewModels.PasswordRecovery
{
	public class ResetViewModel
	{
		public string Token { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		[DisplayName("Password")]
		public string Password { get; set; }

		[Required]
		[DisplayName("Confirm")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}