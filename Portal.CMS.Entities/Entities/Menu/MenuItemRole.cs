using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Portal.CMS.Entities.Entities.Authentication;

namespace Portal.CMS.Entities.Entities.Menu
{
    public class MenuItemRole
    {
        [Key]
        public int MenuItemRoleId { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}