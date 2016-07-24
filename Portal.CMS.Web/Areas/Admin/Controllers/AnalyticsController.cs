using Portal.CMS.Entities.Entities.Analytics;
using Portal.CMS.Services.Analytics;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Analytics;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class AnalyticsController : Controller
    {
        #region Dependencies

        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult TotalHitsToday(ChartSize chartSize)
        {
            var dataSet = _analyticsService.TotalHitsToday();

            var model = new ChartViewModel()
            {
                ChartId = "chart-total-hits-today",
                ChartName = "Total Hits Today",
                ChartSize = chartSize,
                ChartType = ChartType.Donut,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0} ({1})", item.Key, item.Value), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        [ChildActionOnly]
        public ActionResult TotalHitsWeekly(ChartSize chartSize)
        {
            var dataSet = _analyticsService.TotalHitsThisWeek();

            var model = new ChartViewModel()
            {
                ChartId = "chart-total-hits-weekly",
                ChartName = "Total Hits This Week",
                ChartSize = chartSize,
                ChartType = ChartType.Bar,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0})", item.Key), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        [ChildActionOnly]
        public ActionResult TotalHitsMonthly(ChartSize chartSize)
        {
            var dataSet = _analyticsService.TotalHitsThisMonth();

            var model = new ChartViewModel()
            {
                ChartId = "chart-total-hits-monthly",
                ChartName = "Total Hits Per Week This Month",
                ChartSize = chartSize,
                ChartType = ChartType.Bar,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel() { ColumnName = string.Format("{0}", item.Key), ColumnValues = new List<int>() { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        [ChildActionOnly]
        public ActionResult TopPages(ChartSize chartSize, TimePeriod timePeriod)
        {
            var dataSet = _analyticsService.GetTopPages(DetermineTimePeriod(timePeriod));

            var model = new ChartViewModel()
            {
                ChartId = string.Format("chart-top-pages-{0}", timePeriod.ToString().ToLower()),
                ChartName = string.Format("Top Pages ({0})", timePeriod.ToString()),
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

        [ChildActionOnly]
        public ActionResult TopPosts(ChartSize chartSize, TimePeriod timePeriod)
        {
            var dataSet = _analyticsService.GetTopPosts(DetermineTimePeriod(timePeriod));

            var model = new ChartViewModel()
            {
                ChartId = string.Format("chart-top-posts-{0}", timePeriod.ToString().ToLower()),
                ChartName = string.Format("Top Posts ({0})", timePeriod.ToString()),
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

        [ChildActionOnly]
        public ActionResult TopPostCategories(ChartSize chartSize, TimePeriod timePeriod)
        {
            var dataSet = _analyticsService.GetTopPostCategories(DetermineTimePeriod(timePeriod));

            var model = new ChartViewModel()
            {
                ChartId = string.Format("chart-top-post-categories-{0}", timePeriod.ToString().ToLower()),
                ChartName = string.Format("Top Post Categories ({0})", timePeriod.ToString()),
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

        static DateTime? DetermineTimePeriod(TimePeriod timePeriod)
        {
            DateTime? earliest;

            switch (timePeriod)
            {
                case TimePeriod.All:
                    earliest = null;
                    break;

                case TimePeriod.Month:
                    earliest = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    break;

                case TimePeriod.Week:
                    earliest = DateTime.Now.AddDays(-7);
                    break;

                case TimePeriod.Today:
                    earliest = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    break;

                default:
                    earliest = null;
                    break;
            }

            return earliest;
        }
    }
}