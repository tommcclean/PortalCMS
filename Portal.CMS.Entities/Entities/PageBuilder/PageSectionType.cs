using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.PageBuilder
{
    public class PageSectionType
    {
        [Key]
        public int PageSectionTypeId { get; set; }

        [Required]
        public string PageSectionTypeName { get; set; }

        [Required]
        public string PageSectionTypeBody { get; set; }
    }
}