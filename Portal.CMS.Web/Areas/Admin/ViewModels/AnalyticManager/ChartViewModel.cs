using Portal.CMS.Entities.Enumerators;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.AnalyticManager
{
    public class ChartViewModel
    {
        public string ChartId { get; set; }

        public string ChartName { get; set; }

        public ChartSize ChartSize { get; set; }

        public ChartType ChartType { get; set; }

        public List<ColumnViewModel> ChartColumns { get; set; }

        public ChartLinkViewModel ChartLink { get; set; }
    }

    public class ChartLinkViewModel
    {
        public string LinkText { get; set; }

        public string LinkRoute { get; set; }
    }
}