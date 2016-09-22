using Portal.CMS.Entities.Entities.Themes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class ThemeSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var fonts = new List<Font>();

            if (!context.Fonts.Any())
            {
                fonts.Add(new Entities.Themes.Font { FontName = "Portal", FontPath = "/Areas/Admin/Content/Fonts/Emeric.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "AllerDisplay", FontPath = "/Areas/Admin/Content/Fonts/AllerDisplay.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Anagram", FontPath = "/Areas/Admin/Content/Fonts/Anagram.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Architect", FontPath = "/Areas/Admin/Content/Fonts/ArchitectsDaughter.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Blackout", FontPath = "/Areas/Admin/Content/Fonts/Blackout-2am.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "BreeSerif", FontPath = "/Areas/Admin/Content/Fonts/BreeSerif-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Bubblegum", FontPath = "/Areas/Admin/Content/Fonts/BubblegumSans-Regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "CarbonType", FontPath = "/Areas/Admin/Content/Fonts/carbontype.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "DancingScript", FontPath = "/Areas/Admin/Content/Fonts/dancingscript-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "GrandHotel-Regular", FontPath = "/Areas/Admin/Content/Fonts/grandhotel-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Organo", FontPath = "/Areas/Admin/Content/Fonts/Organo.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Railway", FontPath = "/Areas/Admin/Content/Fonts/Railway.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Roboto-Bold", FontPath = "/Areas/Admin/Content/Fonts/roboto-bold.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Roboto-Thin", FontPath = "/Areas/Admin/Content/Fonts/roboto-thin.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Sniglet", FontPath = "/Areas/Admin/Content/Fonts/sniglet-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "ThinLine", FontPath = "/Areas/Admin/Content/Fonts/thin_line_font.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

                context.Fonts.AddRange(fonts);
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

                themes.Add(new Entities.Themes.Theme { ThemeName = "Portal", TitleFontId = defaultFont.FontId, TextFontId = defaultFont.FontId, IsDefault = true, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20 });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Impact", TitleFontId = allerDisplayFont.FontId, TextFontId = snigletFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20 });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Futuristic", TitleFontId = thinLineFont.FontId, TextFontId = robotoThinFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20 });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Grim", TitleFontId = carbonFont.FontId, TextFontId = bubblegumFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20 });
                themes.Add(new Entities.Themes.Theme { ThemeName = "Elegant", TitleFontId = dancingFont.FontId, TextFontId = grandHotelFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20 });

                context.Themes.AddRange(themes);
            }

            context.SaveChanges();
        }
    }
}