using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class PagePartial
    {
        [Key]
        public int PagePartialId { get; set; }

        public string RouteArea { get; set; }

        [Required]
        public string RouteController { get; set; }

        [Required]
        public string RouteAction { get; set; }

        #region Virtual Properties

        public virtual ICollection<PageAssociation> PageAssociations { get; set; }

        #endregion Virtual Properties
    }
}