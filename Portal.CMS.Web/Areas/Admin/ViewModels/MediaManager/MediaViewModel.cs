using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.MediaManager
{
    public class MediaViewModel
    {
        public List<Image> Images { get; set; }

        public List<Font> Fonts { get; set; }
    }
}