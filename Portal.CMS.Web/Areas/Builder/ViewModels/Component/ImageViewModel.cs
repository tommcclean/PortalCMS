using Portal.CMS.Entities.Entities.Generic;
using System.Collections.Generic;
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

        public IEnumerable<Portal.CMS.Entities.Entities.Generic.Image> ImageList { get; set; }
    }
}