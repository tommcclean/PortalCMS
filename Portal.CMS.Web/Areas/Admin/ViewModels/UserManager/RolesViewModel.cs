using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.UserManager
{
    public class RolesViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public List<ApplicationRole> RoleList { get; set; }
    }
}