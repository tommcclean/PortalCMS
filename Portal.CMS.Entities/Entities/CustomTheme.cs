using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class CustomTheme
    {
        [Key]
        public int ThemeId { get; set; }

        [Required]
        public string ThemeName { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        #region Text Settings

        public int? TitleFontId { get; set; }

        [ForeignKey(nameof(TitleFontId))]
        public virtual Font TitleFont { get; set; }

        public int? TextFontId { get; set; }

        [ForeignKey(nameof(TextFontId))]
        public virtual Font TextFont { get; set; }

        [Required, Range(10, 60)]
        public int TitleLargeFontSize { get; set; }

        [Required, Range(10, 60)]
        public int TitleMediumFontSize { get; set; }

        [Required, Range(10, 60)]
        public int TitleSmallFontSize { get; set; }

        [Required, Range(10, 60)]
        public int TitleTinyFontSize { get; set; }

        [Required, Range(10, 60)]
        public int TextStandardFontSize { get; set; }

        #endregion Text Settings

        #region Colour Settings

        [Required]
        public string PageBackgroundColour { get; set; }

        [Required]
        public string MenuBackgroundColour { get; set; }

        [Required]
        public string MenuTextColour { get; set; }

        #endregion Colour Settings

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}