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
                fonts.Add(new Entities.Themes.Font { FontName = "BreeSerif", FontPath = "/Areas/Admin/Content/Fonts/BreeSerif.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "Bubblegum", FontPath = "/Areas/Admin/Content/Fonts/BubblegumSans-Regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "CarbonType", FontPath = "/Areas/Admin/Content/Fonts/carbontype.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "DancingScript", FontPath = "/Areas/Admin/Content/Fonts/dancingscript-regular.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                fonts.Add(new Entities.Themes.Font { FontName = "GrandHotel-Regular", FontPath = "/Areas/Admin/Content/Fonts/BreeSerif.otf", FontType = "opentype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
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
                var defaultfont = context.Fonts.First(x => x.FontName == "Portal");

                themes.Add(new Entities.Themes.Theme { ThemeName = "Portal", TextFontId = defaultfont.FontId, TitleFontId = defaultfont.FontId, IsDefault = true, DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

                context.Themes.AddRange(themes);
            }

            context.SaveChanges();
        }
    }
}