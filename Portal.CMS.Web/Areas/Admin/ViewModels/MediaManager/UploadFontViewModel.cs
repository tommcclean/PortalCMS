using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.MediaManager
{
    public class UploadFontViewModel
    {
        [Required]
        [DisplayName("Font Name")]
        public string FontName { get; set; }

        [Required]
        [DisplayName("Font Type")]
        public string FontType { get; set; }

        [DisplayName("Font")]
        [Required]
        public HttpPostedFileBase AttachedFont { get; set; }
    }
}