using System;
using System.Collections.Generic;
using System.Linq;
using Portal.CMS.Entities.Entities.Themes;

namespace Portal.CMS.Entities.Seed
{
    public static class ThemeSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var fonts = new List<Font>();

            if (!context.Fonts.Any())
            {
                fonts.Add(new Entities.Themes.Font { FontName = "Portal", FontPath = "/Content/Fonts/Emeric.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "AllerDisplay", FontPath = "/Content/Fonts/AllerDisplay.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Anagram", FontPath = "/Content/Fonts/Anagram.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Architect", FontPath = "/Content/Fonts/ArchitectsDaughter.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Blackout", FontPath = "/Content/Fonts/Blackout-2am.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "BreeSerif", FontPath = "/Content/Fonts/BreeSerif-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Bubblegum", FontPath = "/Content/Fonts/BubblegumSans-Regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "CarbonType", FontPath = "/Content/Fonts/carbontype.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "DancingScript", FontPath = "/Content/Fonts/dancingscript-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "GrandHotel-Regular", FontPath = "/Content/Fonts/grandhotel-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Organo", FontPath = "/Content/Fonts/Organo.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Railway", FontPath = "/Content/Fonts/Railway.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Roboto-Bold", FontPath = "/Content/Fonts/roboto-bold.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Roboto-Thin", FontPath = "/Content/Fonts/roboto-thin.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Sniglet", FontPath = "/Content/Fonts/sniglet-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "ThinLine", FontPath = "/Content/Fonts/thin_line_font.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

                context.Fonts.AddRange(fonts);
            }

            if (context.Fonts.Any(x => x.FontPath.Contains("Area")))
            {
                foreach (var font in context.Fonts)
                {
                    font.FontPath = font.FontPath.Replace("/Areas/Admin/Content/Fonts", "/Content/Fonts");
                }
            }

            context.SaveChanges();

            var themes = new List<Theme>();

            if (!context.Themes.Any())
            {
                var fontList = context.Fonts.ToList();

                var defaultFont = fontList.First(x => x.FontName == "Portal");
                var allerDisplayFont = fontList.First(x => x.FontName == "AllerDisplay");
                var snigletFont = fontList.First(x => x.FontName == "Sniglet");
                var thinLineFont = fontList.First(x => x.FontName == "ThinLine");
                var robotoThinFont = fontList.First(x => x.FontName == "Roboto-Thin");
                var carbonFont = fontList.First(x => x.FontName == "CarbonType");
                var bubblegumFont = fontList.First(x => x.FontName == "Bubblegum");
                var dancingFont = fontList.First(x => x.FontName == "DancingScript");
                var grandHotelFont = fontList.First(x => x.FontName == "GrandHotel-Regular");

                themes.Add(new Entities.Themes.Theme { ThemeName = "Portal", TitleFontId = defaultFont.FontId, TextFontId = defaultFont.FontId, IsDefault = true, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Impact", TitleFontId = allerDisplayFont.FontId, TextFontId = snigletFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Futuristic", TitleFontId = thinLineFont.FontId, TextFontId = robotoThinFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Grim", TitleFontId = carbonFont.FontId, TextFontId = bubblegumFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Elegant", TitleFontId = dancingFont.FontId, TextFontId = grandHotelFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });

                context.Themes.AddRange(themes);
            }

            context.SaveChanges();
        }
    }
}