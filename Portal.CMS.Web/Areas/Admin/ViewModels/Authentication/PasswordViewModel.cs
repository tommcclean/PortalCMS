using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Authentication
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