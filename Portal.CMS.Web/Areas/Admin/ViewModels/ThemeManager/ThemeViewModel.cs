using Portal.CMS.Entities.Entities.Themes;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class ThemeViewModel
    {
        public IEnumerable<Theme> Themes { get; set; }

        public IEnumerable<Font> Fonts { get; set; }
    }
}