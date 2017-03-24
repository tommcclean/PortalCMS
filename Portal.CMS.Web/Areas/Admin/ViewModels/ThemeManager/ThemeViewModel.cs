using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class ThemeViewModel
    {
        public IEnumerable<CustomTheme> Themes { get; set; }

        public IEnumerable<Font> Fonts { get; set; }
    }
}