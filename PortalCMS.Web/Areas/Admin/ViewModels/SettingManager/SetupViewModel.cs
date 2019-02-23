using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.SettingManager
{
	public class SetupViewModel
	{
		[Required]
		[DisplayName("Website Name")]
		public string WebsiteName { get; set; }

		[DisplayName("Website Logo")]
		public string AttachedLogo { get; set; }

		[DisplayName("Website Fav Icon")]
		public string AttachedIcon { get; set; }

		[Required]
		[DisplayName("Website Description")]
		public string WebsiteDescription { get; set; }

		[DisplayName("Google Tracking Code")]
		public string GoogleAnalyticsId { get; set; }

		[DisplayName("Email From Address")]
		public string EmailFromAddress { get; set; }

		[DisplayName("SendGrid API Key")]
		public string SendGridApiKey { get; set; }

		[DisplayName("CDN Address")]
		public string CDNAddress { get; set; }
	}
}