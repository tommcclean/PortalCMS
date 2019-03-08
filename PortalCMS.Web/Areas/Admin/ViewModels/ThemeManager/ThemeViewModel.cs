using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class ThemeViewModel
    {
        public IEnumerable<CustomTheme> Themes { get; set; }

        public IEnumerable<Font> Fonts { get; set; }
    }
}