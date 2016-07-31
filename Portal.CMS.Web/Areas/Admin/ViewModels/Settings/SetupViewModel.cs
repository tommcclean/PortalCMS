using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Settings
{
    public class SetupViewModel
    {
        [Required]
        [DisplayName("Website Name")]
        public string WebsiteName { get; set; }

        [Required]
        [DisplayName("Website Description")]
        public string WebsiteDescription { get; set; }

        [DisplayName("Google Tracking Code")]
        public string GoogleAnalyticsId { get; set; }

        [DisplayName("Email From Address")]
        public string EmailFromAddress { get; set; }

        [DisplayName("SendGrid User Name")]
        public string SendGridUserName { get; set; }

        [DisplayName("SendGrid Password")]
        public string SendGridPassword { get; set; }
    }
}