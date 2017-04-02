using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Profile.ViewModels.Manage
{
    public class AccountViewModel
    {
        [Required]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string GivenName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string FamilyName { get; set; }
    }
}