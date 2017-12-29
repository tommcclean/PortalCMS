using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace Portal.CMS.Web.Areas.PageBuilder.ViewModels.Association
{
    public class EditAccessViewModel
    {
        public int PageAssociationId { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public List<Role> RoleList { get; set; }
    }
}