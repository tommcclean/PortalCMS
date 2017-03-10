using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Entities.Entities.PageBuilder;

namespace Portal.CMS.Entities.Entities.Posts
{
    public class PageRole
    {
        [Key]
        public int PageRoleId { get; set; }

        [ForeignKey("Page")]
        public int PageId { get; set; }

        public virtual Page Page { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}