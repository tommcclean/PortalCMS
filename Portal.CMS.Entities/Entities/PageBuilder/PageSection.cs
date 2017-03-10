using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.PageBuilder
{
    public class PageSection
    {
        [Key]
        public int PageSectionId { get; set; }

        public string PageSectionBody { get; set; }

        #region Virtual Properties

        public virtual ICollection<PageSectionRole> PageSectionRoles { get; set; }

        public virtual ICollection<PageSectionBackup> PageSectionBackups { get; set; }

        public virtual ICollection<PageAssociation> PageAssociations { get; set; }

        #endregion Virtual Properties
    }
}