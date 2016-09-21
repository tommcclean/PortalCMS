using Portal.CMS.Entities.Entities.Themes;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Shared
{
    public class ThemeManagerViewModel
    {
        public int PageId { get; set; }

        public IEnumerable<Portal.CMS.Entities.Entities.Themes.Theme> Themes { get; set; }

        public List<Font> Fonts { get; set; }
    }
}