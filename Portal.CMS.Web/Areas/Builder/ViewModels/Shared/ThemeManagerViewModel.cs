using Portal.CMS.Entities.Entities.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Shared
{
    public class ThemeManagerViewModel
    {
        public int PageId { get; set; }

        public IEnumerable<Theme> Themes { get; set; }

        public List<Font> Fonts { get; set; }
    }
}