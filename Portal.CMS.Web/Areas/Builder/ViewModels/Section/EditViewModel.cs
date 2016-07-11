using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;
using System.ComponentModel;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Section
{
    public class EditViewModel
    {
        public int PageId { get; set; }

        public int SectionId { get; set; }

        public int BackgroundImageId { get; set; }

        [DisplayName("Height")]
        public PageSectionHeight PageSectionHeight { get; set; }

        [DisplayName("Background")]
        public PageSectionBackgroundType PageSectionBackgroundType { get; set; }

        public IEnumerable<Portal.CMS.Entities.Entities.Generic.Image> ImageList { get; set; }
    }
}