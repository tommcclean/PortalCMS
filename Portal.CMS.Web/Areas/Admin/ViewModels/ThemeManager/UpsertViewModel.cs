using Portal.CMS.Entities.Entities.Themes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class UpsertViewModel
    {
        public int ThemeId { get; set; }

        [Required]
        [DisplayName("Theme Name")]
        public string ThemeName { get; set; }

        [Required]
        [DisplayName("Title Font")]
        public int TitleFontId { get; set; }

        [Required]
        [DisplayName("Text Font")]
        public int TextFontId { get; set; }

        public bool IsDefault { get; set; }

        public IEnumerable<Font> FontList { get; set; }
    }
}