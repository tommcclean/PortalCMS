using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Section
{
    public class EditPartialViewModel
    {
        public int PageAssociationId { get; set; }

        public int PagePartialId { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        #region Enumerable Properties

        public List<Role> RoleList { get; set; }

        #endregion Enumerable Properties
    }
}