using Portal.CMS.Entities.Enumerators;
using Portal.CMS.Web.ViewModels.Shared;
using System.ComponentModel;

namespace Portal.CMS.Web.Areas.PageBuilder.ViewModels.Section
{
    public class EditSectionViewModel
    {
        public int PageAssociationId { get; set; }

        public int SectionId { get; set; }

        public string BackgroundType { get; set; }

        public int BackgroundImageId { get; set; }

        public PaginationViewModel MediaLibrary { get; set; }

        [DisplayName("Background Colour")]
        public string BackgroundColour { get; set; }

        [DisplayName("Height")]
        public PageSectionHeight PageSectionHeight { get; set; }

        [DisplayName("Background Style")]
        public PageSectionBackgroundStyle PageSectionBackgroundStyle { get; set; }

        [DisplayName("Category")]
        public ImageCategory ImageCategory { get; set; }
    }
}