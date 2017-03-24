using Portal.CMS.Entities.Entities;
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

        #region Colour Settings

        [Required]
        [DisplayName("Page Background Colour")]
        public string PageBackgroundColour { get; set; }

        [Required]
        [DisplayName("Menu Background Colour")]
        public string MenuBackgroundColour { get; set; }

        [Required]
        [DisplayName("Menu Text Colour")]
        public string MenuTextColour { get; set; }

        #endregion Colour Settings

        #region Text Settings

        [Required]
        [DisplayName("Title Font")]
        public int TitleFontId { get; set; }

        [Required]
        [DisplayName("Text Font")]
        public int TextFontId { get; set; }

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

        #endregion Text Settings

        public bool IsDefault { get; set; }

        public IEnumerable<Font> FontList { get; set; }
    }
}