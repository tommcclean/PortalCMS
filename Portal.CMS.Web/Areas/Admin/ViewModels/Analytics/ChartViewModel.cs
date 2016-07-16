using Portal.CMS.Entities.Entities.Analytics;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Analytics
{
    public class ChartViewModel
    {
        public string ChartId { get; set; }

        public string ChartName { get; set; }

        public ChartSize ChartSize { get; set; }

        public ChartType ChartType { get; set; }

        public List<ColumnViewModel> ChartColumns { get; set; }
    }
}