using Portal.CMS.Web.ViewModels.Shared;

namespace Portal.CMS.Web.Areas.PageBuilder.ViewModels.Component
{
    public class ImageViewModel
    {
        public int SectionId { get; set; }

        public string ElementType { get; set; }

        public string ElementId { get; set; }

        public int SelectedImageId { get; set; }

        #region Enumerable Properties

        public PaginationViewModel GeneralImages { get; set; } = new PaginationViewModel();

        public PaginationViewModel IconImages { get; set; } = new PaginationViewModel();

        public PaginationViewModel ScreenshotImages { get; set; } = new PaginationViewModel();

        public PaginationViewModel TextureImages { get; set; } = new PaginationViewModel();

        #endregion Enumerable Properties
    }
}