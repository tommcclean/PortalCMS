using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities.PageBuilder
{
    public class PageSection
    {
        [Key]
        public int PageSectionId { get; set; }

        [Required]
        [ForeignKey("Page")]
        public int PageId { get; set; }

        public virtual Page Page { get; set; }

        [Required]
        [ForeignKey("PageSectionType")]
        public int PageSectionTypeId { get; set; }

        public virtual PageSectionType PageSectionType { get; set; }

        public string PageSectionBody { get; set; }

        public int PageSectionOrder { get; set; }

        public virtual ICollection<PageSectionRole> PageSectionRoles { get; set; }
    }
}