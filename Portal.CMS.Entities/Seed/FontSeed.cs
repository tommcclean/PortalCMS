using Portal.CMS.Entities.Entities.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.CMS.Entities.Seed
{
     public static class FontSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Fonts.Any())
            {
                var fonts = new List<Font>();

                fonts.Add(new Entities.Themes.Font { FontName = "Portal CMS", FontPath = "/Areas/Admin/Content/Fonts/Custom/Emeric.ttf", FontType = "truetype", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

                context.Fonts.AddRange(fonts);
            }
        }
    }
}
