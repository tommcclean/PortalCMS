using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.AnalyticManager
{
    public class ColumnViewModel
    {
        public string ColumnName { get; set; }

        public List<int> ColumnValues { get; set; }
    }
}