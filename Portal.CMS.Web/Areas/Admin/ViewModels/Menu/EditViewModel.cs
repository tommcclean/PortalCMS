using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Menu
{
    public class EditViewModel
    {
        public int MenuId { get; set; }

        [Required]
        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        public List<Entities.Entities.Menu.MenuItem> MenuItems { get; set; }
    }
}