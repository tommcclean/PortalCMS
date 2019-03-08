using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class AppDrawerViewModel
    {
        public IEnumerable<CustomTheme> Themes { get; set; }

        public List<Font> Fonts { get; set; }
    }
}