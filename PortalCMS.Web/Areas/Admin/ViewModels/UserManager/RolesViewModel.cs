using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace PortalCMS.Web.Areas.Admin.ViewModels.UserManager
{
    public class RolesViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public List<ApplicationRole> RoleList { get; set; }
    }
}