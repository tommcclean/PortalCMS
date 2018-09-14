using Portal.CMS.Entities.Entities;
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
                fonts.Add(new Font { FontName = "Portal", FontPath = "/Content/Fonts/Emeric.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "AllerDisplay", FontPath = "/Content/Fonts/AllerDisplay.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Anagram", FontPath = "/Content/Fonts/Anagram.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Architect", FontPath = "/Content/Fonts/ArchitectsDaughter.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Blackout", FontPath = "/Content/Fonts/Blackout-2am.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "BreeSerif", FontPath = "/Content/Fonts/BreeSerif-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Bubblegum", FontPath = "/Content/Fonts/BubblegumSans-Regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "CarbonType", FontPath = "/Content/Fonts/carbontype.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "DancingScript", FontPath = "/Content/Fonts/dancingscript-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "GrandHotel-Regular", FontPath = "/Content/Fonts/grandhotel-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Organo", FontPath = "/Content/Fonts/Organo.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Railway", FontPath = "/Content/Fonts/Railway.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Roboto-Bold", FontPath = "/Content/Fonts/roboto-bold.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "Sniglet", FontPath = "/Content/Fonts/sniglet-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Font { FontName = "OpenSans-Regular", FontPath = "/Content/Fonts/OpenSans-Regular.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

                context.Fonts.AddRange(fonts);
            }

            context.SaveChanges();

            var themes = new List<CustomTheme>();

            if (!context.Themes.Any())
            {
                var fontList = context.Fonts.ToList();

                var defaultFont = fontList.First(x => x.FontName == "Portal");
                var allerDisplayFont = fontList.First(x => x.FontName == "AllerDisplay");
                var snigletFont = fontList.First(x => x.FontName == "Sniglet");

                themes.Add(new CustomTheme { ThemeName = "Portal", TitleFontId = defaultFont.FontId, TextFontId = defaultFont.FontId, IsDefault = true, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });
                themes.Add(new CustomTheme { ThemeName = "Impact", TitleFontId = allerDisplayFont.FontId, TextFontId = snigletFont.FontId, IsDefault = false, DateAdded = DateTime.Now, DateUpdated = DateTime.Now, TitleLargeFontSize = 35, TitleMediumFontSize = 35, TitleSmallFontSize = 24, TitleTinyFontSize = 22, TextStandardFontSize = 20, PageBackgroundColour = "#000000", MenuBackgroundColour = "#000000", MenuTextColour = "#9d9d9d" });

                context.Themes.AddRange(themes);
            }

            context.SaveChanges();
        }
    }
}