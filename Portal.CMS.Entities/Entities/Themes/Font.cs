using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.CMS.Entities.Entities.Themes
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
