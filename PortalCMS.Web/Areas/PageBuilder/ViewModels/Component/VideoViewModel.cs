using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.PageBuilder.ViewModels.Component
{
    public class VideoViewModel
    {
        public int SectionId { get; set; }

        public string WidgetWrapperElementId { get; set; }

        public string VideoPlayerElementId { get; set; }

        [Required]
        [DisplayName("Embed URL")]
        public string VideoUrl { get; set; }
    }
}