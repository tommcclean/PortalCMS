using PortalCMS.Entities.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace PortalCMS.Web.Areas.PageBuilder.ViewModels.Association
{
	public class EditAccessViewModel
    {
        public int PageAssociationId { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public List<ApplicationRole> RoleList { get; set; }
    }
}