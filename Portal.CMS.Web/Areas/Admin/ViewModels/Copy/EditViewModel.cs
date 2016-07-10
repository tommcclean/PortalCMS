using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Copy
{
    public class EditViewModel
    {
        public int CopyId { get; set; }

        [Required]
        [DisplayName("Copy Name")]
        public string CopyName { get; set; }

        [Required]
        [DisplayName("Copy Body")]
        public string CopyBody { get; set; }
    }
}