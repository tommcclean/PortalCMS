using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities.Menu
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }

        [Required]
        public string LinkText { get; set; }

        [Required]
        public string LinkAction { get; set; }

        [Required]
        public string LinkController { get; set; }

        public string LinkArea { get; set; }
    }
}