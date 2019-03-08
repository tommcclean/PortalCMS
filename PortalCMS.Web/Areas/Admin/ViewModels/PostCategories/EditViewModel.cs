using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.PostCategories
{
    public class EditViewModel
    {
        public int PostCategoryId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string PostCategoryName { get; set; }
    }
}