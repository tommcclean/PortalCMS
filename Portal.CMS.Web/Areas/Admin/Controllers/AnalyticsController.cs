using Portal.CMS.Entities.Entities.Analytics;
using Portal.CMS.Services.Analytics;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Analytics;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class AnalyticsController : Controller
    {
        #region Dependencies

        private readonly AnalyticsService _analyticsService;

        public AnalyticsController(AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageViewsByDay(ChartSize chartSize)
        {
            var dataSet = _analyticsService.GetPageViewsByDay();

            var model = new ChartViewModel()
            {
                ChartId = "chart-page-views-by-date",
                ChartName = "Page Views This Week",
                ChartSize = chartSize,
                ChartType = ChartType.Bar,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0} ({1})", item.Key, item.Value), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        public ActionResult TopPages(ChartSize chartSize)
        {
            var dataSet = _analyticsService.GetTopPages();

            var model = new ChartViewModel()
            {
                ChartId = "chart-top-pages",
                ChartName = "Top Pages",
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0} ({1})", item.Key, item.Value), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        public ActionResult TopPosts(ChartSize chartSize)
        {
            var dataSet = _analyticsService.GetTopPosts();

            var model = new ChartViewModel()
            {
                ChartId = "chart-top-posts",
                ChartName = "Top Posts",
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0} ({1})", item.Key, item.Value), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        public ActionResult TopPostCategories(ChartSize chartSize)
        {
            var dataSet = _analyticsService.GetTopPostCategories();

            var model = new ChartViewModel()
            {
                ChartId = "chart-top-post-categories",
                ChartName = "Top Post Categories",
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0} ({1})", item.Key, item.Value), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }
    }
}