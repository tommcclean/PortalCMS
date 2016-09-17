using Portal.CMS.Entities.Entities.Generic;
using Portal.CMS.Entities.Entities.Themes;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Media
{
    public class MediaViewModel
    {
        public List<Image> Images { get; set; }

        public List<Font> Fonts { get; set; }
    }
}