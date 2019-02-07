using Portal.CMS.Entities.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class PageRole
    {
        [Key]
        public int PageRoleId { get; set; }

        [ForeignKey("Page")]
        public int PageId { get; set; }

        public virtual Page Page { get; set; }

        [ForeignKey("Role")]
        public string RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}