using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Image
{
    public class EditViewModel
    {
        public int PageId { get; set; }

        public int SectionId { get; set; }

        public string ElementId { get; set; }

        public int SelectedImageId { get; set; }

        public IEnumerable<Portal.CMS.Entities.Entities.Generic.Image> ImageList { get; set; }
    }
}