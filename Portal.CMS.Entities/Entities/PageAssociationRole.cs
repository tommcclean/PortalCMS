using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class PageAssociationRole
    {
        [Key]
        public int PageAssociationRoleId { get; set; }

        [ForeignKey(nameof(PageAssociation))]
        public int PageAssociationId { get; set; }

        public virtual PageAssociation PageAssociation { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}