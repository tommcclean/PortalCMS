using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Menu
{
    public class CreateViewModel
    {
        [Required]
        [DisplayName("Menu Name")]
        public string MenuName { get; set; }
    }
}