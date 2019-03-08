using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.MediaManager
{
    public class MediaViewModel
    {
        public List<Image> Images { get; set; }

        public List<Font> Fonts { get; set; }
    }
}