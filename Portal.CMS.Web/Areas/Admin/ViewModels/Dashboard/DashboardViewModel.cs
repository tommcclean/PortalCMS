using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public List<Image> Media { get; set; }

        public Post LatestPost { get; set; }
    }
}