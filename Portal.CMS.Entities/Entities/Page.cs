using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }

        [Required]
        public string PageName { get; set; }

        public string PageArea { get; set; }

        [Required]
        public string PageController { get; set; }

        [Required]
        public string PageAction { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }

        #region Virtual Properties

        public virtual ICollection<PageAssociation> PageAssociations { get; set; }

        public virtual ICollection<PageRole> PageRoles { get; set; }

        #endregion Virtual Properties
    }
}