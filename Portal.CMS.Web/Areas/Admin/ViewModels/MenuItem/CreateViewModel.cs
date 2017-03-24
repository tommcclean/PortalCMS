using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.MenuItem
{
    public class CreateViewModel
    {
        [Required]
        [DisplayName("Menu")]
        public int MenuId { get; set; }

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

        public List<Role> RoleList { get; set; }

        public IEnumerable<MenuSystem> MenuList { get; set; }

        public IEnumerable<Page> PageList { get; set; }

        public IEnumerable<Post> PostList { get; set; }
    }
}