using Portal.CMS.Entities.Entities.Generic;
using Portal.CMS.Web.ViewModels.Shared;
using System.ComponentModel;
using System.Web;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Component
{
    public class ImageViewModel
    {
        public int PageId { get; set; }

        public int SectionId { get; set; }

        public string ElementType { get; set; }

        public string ElementId { get; set; }

        public int SelectedImageId { get; set; }

        [DisplayName("Attach Image")]
        public HttpPostedFileBase AttachedImage { get; set; }

        [DisplayName("Category")]
        public ImageCategory ImageCategory { get; set; }

        public PaginationViewModel GeneralImages { get; set; } = new PaginationViewModel();

        public PaginationViewModel IconImages { get; set; } = new PaginationViewModel();

        public PaginationViewModel ScreenshotImages { get; set; } = new PaginationViewModel();

        public PaginationViewModel TextureImages { get; set; } = new PaginationViewModel();
    }
}