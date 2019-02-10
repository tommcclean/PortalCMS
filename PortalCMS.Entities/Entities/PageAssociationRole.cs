using PortalCMS.Entities.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalCMS.Entities.Entities
{
    public class PageAssociationRole
    {
        [Key]
        public int PageAssociationRoleId { get; set; }

        [ForeignKey(nameof(PageAssociation))]
        public int PageAssociationId { get; set; }

        public virtual PageAssociation PageAssociation { get; set; }

        [ForeignKey(nameof(Role))]
        public string RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}