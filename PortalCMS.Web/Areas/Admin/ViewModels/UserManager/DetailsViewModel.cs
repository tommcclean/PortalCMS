using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.UserManager
{
    public class DetailsViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Email")]
        [Required]
        public string EmailAddress { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string GivenName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string FamilyName { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}