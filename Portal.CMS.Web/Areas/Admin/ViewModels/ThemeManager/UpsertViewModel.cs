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

        #region Text

        [Required]
        [DisplayName("Large Title Font Size")]
        public int LargeTitleFontSize { get; set; } = 35;

        [Required]
        [DisplayName("Medium Title Font Size")]
        public int MediumTitleFontSize { get; set; } = 35;

        [Required]
        [DisplayName("Small Title Font Size")]
        public int SmallTitleFontSize { get; set; } = 24;

        [Required]
        [DisplayName("Tiny Title Font Size")]
        public int TinyTitleFontSize { get; set; } = 22;

        [Required]
        [DisplayName("Standard Text Font Size")]
        public int TextStandardFontSize { get; set; } = 20;

        #endregion Text

        public bool IsDefault { get; set; }

        public IEnumerable<Font> FontList { get; set; }
    }
}