using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Authentication.ViewModels.Registration
{
    public class RegisterViewModel
    {
        [DisplayName("Email")]
        [Required]
        public string EmailAddress { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string GivenName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string FamilyName { get; set; }
    }
}