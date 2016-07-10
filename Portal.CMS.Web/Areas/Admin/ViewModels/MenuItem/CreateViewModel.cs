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
        [DisplayName("Link Text")]
        public string LinkText { get; set; }

        [Required]
        [DisplayName("Action")]
        public string LinkAction { get; set; }

        [Required]
        [DisplayName("Controller")]
        public string LinkController { get; set; }

        [DisplayName("Area")]
        public string LinkArea { get; set; }

        public IEnumerable<Entities.Entities.Menu.Menu> MenuList { get; set; }
    }
}