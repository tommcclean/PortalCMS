using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.RoleManager
{
    public class EditViewModel
    {
        public string RoleId { get; set; }

        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}