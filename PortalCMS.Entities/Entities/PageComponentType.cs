using PortalCMS.Entities.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Entities.Entities
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
        public string PageComponentBody { get; set; }
    }
}