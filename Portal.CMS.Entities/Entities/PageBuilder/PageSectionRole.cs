using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Entities.Entities.PageBuilder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities.PageBuilder
{
    public class PageSectionRole
    {
        [Key]
        public int PageSectionRoleId { get; set; }

        [ForeignKey(nameof(PageSection))]
        public int PageSectionId { get; set; }

        public virtual PageSection PageSection { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}