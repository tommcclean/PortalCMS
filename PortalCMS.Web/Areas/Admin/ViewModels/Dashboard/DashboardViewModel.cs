using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public List<Image> Media { get; set; }

        public Post LatestPost { get; set; }
    }
}