using Microsoft.AspNet.Identity.EntityFramework;
using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.MenuItem
{
    public class EditViewModel
    {
        [Required]
        [DisplayName("Text")]
        public string LinkText { get; set; }

        [DisplayName("Icon")]
        public string LinkIcon { get; set; }

        [Required]
        [DisplayName("Link URL")]
        public string LinkURL { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public int MenuId { get; set; }

        public int MenuItemId { get; set; }

        public List<ApplicationRole> RoleList { get; set; }
    }
}