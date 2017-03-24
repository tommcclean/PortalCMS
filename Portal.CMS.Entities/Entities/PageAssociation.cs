using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class PageAssociation
    {
        [Key]
        public int PageAssociationId { get; set; }

        [Required]
        [ForeignKey("Page")]
        public int PageId { get; set; }

        [ForeignKey("PageSection")]
        public int? PageSectionId { get; set; }

        [ForeignKey("PagePartial")]
        public int? PagePartialId { get; set; }

        [Required]
        public int PageAssociationOrder { get; set; }

        #region Virtual Properties

        public virtual Page Page { get; set; }

        public virtual PageSection PageSection { get; set; }

        public virtual PagePartial PagePartial { get; set; }

        public virtual ICollection<PageAssociationRole> PageAssociationRoles { get; set; }

        #endregion Virtual Properties
    }
}