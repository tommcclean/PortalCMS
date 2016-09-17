using Portal.CMS.Entities.Entities.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.ThemeManager
{
    public class ThemeViewModel
    {
        public IEnumerable<Theme> Themes { get; set; }
    }
}