using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Section
{
    public class CloneViewModel
    {
        [Required]
        [DisplayName("Page")]
        public int PageId { get; set; }

        public int PageAssociationId { get; set; }

        public List<Page> PageList { get; set; }
    }
}