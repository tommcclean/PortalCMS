using Portal.CMS.Entities.Entities.Themes;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class AppDrawerViewModel
    {
        public IEnumerable<Portal.CMS.Entities.Entities.Themes.Theme> Themes { get; set; }

        public List<Font> Fonts { get; set; }
    }
}