using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.PageBuilder
{
    public class PageComponentType
    {
        [Key]
        public int PageComponentTypeId { get; set; }

        [Required]
        public string PageComponentTypeName { get; set; }

        [Required]
        public PageComponentTypeCategory PageComponentTypeCategory { get; set; }

        [Required]
        public string PageComponentTypeDescription { get; set; }

        [Required]
        public string PageComponentBody { get; set; }
    }
}