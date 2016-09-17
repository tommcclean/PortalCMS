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
                fonts.Add(new Entities.Themes.Font { FontName = "Portal", FontPath = "/Areas/Admin/Content/Fonts/Uploads/Emeric.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

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