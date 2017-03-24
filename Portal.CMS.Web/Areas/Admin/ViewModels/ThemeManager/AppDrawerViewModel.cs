using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class AppDrawerViewModel
    {
        public IEnumerable<CustomTheme> Themes { get; set; }

        public List<Font> Fonts { get; set; }
    }
}