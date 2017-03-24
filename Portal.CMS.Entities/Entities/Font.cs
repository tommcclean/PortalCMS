using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class Font
    {
        [Key]
        public int FontId { get; set; }

        [Required]
        public string FontName { get; set; }

        [Required]
        public string FontType { get; set; }

        [Required]
        public string FontPath { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}