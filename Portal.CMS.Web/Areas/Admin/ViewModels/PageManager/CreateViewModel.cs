using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.PageManager
{
    public class CreateViewModel
    {
        [Required]
        [DisplayName("Page Name")]
        public string PageName { get; set; }

        [DisplayName("Area")]
        public string PageArea { get; set; }

        [Required]
        [DisplayName("Controller")]
        public string PageController { get; set; }

        [Required]
        [DisplayName("Action")]
        public string PageAction { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public List<Role> RoleList { get; set; }
    }
}