using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.AnalyticManager
{
    public class ColumnViewModel
    {
        public string ColumnName { get; set; }

        public List<int> ColumnValues { get; set; }
    }
}