using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Authentication.ViewModels.Login
{
    public class LoginViewModel
    {
        [DisplayName("Email")]
        [Required]
        public string EmailAddress { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
    }
}