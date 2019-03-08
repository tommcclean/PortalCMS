using PortalCMS.Entities.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalCMS.Entities.Entities
{
    public class MenuItemRole
    {
        [Key]
        public int MenuItemRoleId { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}