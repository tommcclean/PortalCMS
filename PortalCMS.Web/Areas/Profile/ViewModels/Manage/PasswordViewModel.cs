using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Profile.ViewModels.Manage
{
    public class PasswordViewModel
    {
        [Required]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}