using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Authentication.ViewModels.Recovery
{
    public class ResetViewModel
    {
        public string Token { get; set; }

        [Required]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm")]
        public string ConfirmPassword { get; set; }
    }
}