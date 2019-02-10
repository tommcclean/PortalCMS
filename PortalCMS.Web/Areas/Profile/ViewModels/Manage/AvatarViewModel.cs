using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PortalCMS.Web.Areas.Profile.ViewModels.Manage
{
	public class AvatarViewModel
	{
		[Required]
		[DisplayName("Avatar")]
		public HttpPostedFileBase AttachedImage { get; set; }
	}
}