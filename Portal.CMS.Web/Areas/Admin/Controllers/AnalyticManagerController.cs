using LogBook.Services;
using Portal.CMS.Entities.Enumerators;
using Portal.CMS.Services.Analytics;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.AnalyticManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter(ActionFilterResponseType.Page)]
    public class AnalyticManagerController : Controller
    {
        #region Manifest Constants

        private const string DISPLAY_CHART_VIEW = "_DisplayChart";

        #endregion Manifest Constants

        #region Dependencies

        private readonly IAnalyticsService _analyticsService;

        public AnalyticManagerController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogEntries()
        {
            var logHandler = new LogHandler();

            var model = new LogEntriesViewModel
            {
                LogEntries = logHandler.ReadLatestLogEntries(100)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ReviewException(int logEntryId)
        {
            var logHandler = new LogHandler();

            var logEntry = logHandler.GetLogEntry(logEntryId);

            var model = new ReviewExceptionViewModel
            {
                LogEntryId = logEntryId,
                LogException = logEntry.LogExceptions.First().ExceptionDetail
            };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult TotalHitsToday(ChartSize chartSize)
        {
            var dataSet = _analyticsService.TotalHitsToday();

            var model = new ChartViewModel
            {
                ChartId = "chart-total-hits-today",
                ChartName = "Total Hits Today",
                ChartSize = chartSize,
                ChartType = ChartType.Donut,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key} ({item.Value})", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView(DISPLAY_CHART_VIEW, model);
        }

        [ChildActionOnly]
        public ActionResult ErrorPercentage(string chartName, ChartSize chartSize, DateTime sinceDate)
        {
            var dataSet = _analyticsService.ErrorPercentage(sinceDate);

            var model = new ChartViewModel
            {
                ChartId = $"chart-error-percentage-{sinceDate.ToString("yyyyMMhhhddmm")}",
                ChartName = chartName,
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>(),
                ChartLink = new ChartLinkViewModel { LinkText = "More Details", LinkRoute = Url.Action(nameof(LogEntries), "AnalyticManager") }
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key} ({item.Value})", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView(DISPLAY_CHART_VIEW, model);
        }

        [ChildActionOnly]
        public ActionResult TotalHitsWeekly(ChartSize chartSize)
        {
            var dataSet = _analyticsService.TotalHitsThisWeek();

            var model = new ChartViewModel
            {
                ChartId = "chart-total-hits-weekly",
                ChartName = "Total Hits This Week",
                ChartSize = chartSize,
                ChartType = ChartType.Bar,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key})", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView(DISPLAY_CHART_VIEW, model);
        }

        [ChildActionOnly]
        public ActionResult TotalHitsMonthly(ChartSize chartSize)
        {
            var dataSet = _analyticsService.TotalHitsThisMonth();

            var model = new ChartViewModel
            {
                ChartId = "chart-total-hits-monthly",
                ChartName = "Total Hits Per Week This Month",
                ChartSize = chartSize,
                ChartType = ChartType.Bar,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key}", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        [ChildActionOnly]
        public ActionResult TopPages(ChartSize chartSize, TimePeriod timePeriod)
        {
            var dataSet = _analyticsService.GetTopPages(DetermineTimePeriod(timePeriod));

            var model = new ChartViewModel
            {
                ChartId = $"chart-top-pages-{timePeriod.ToString().ToLower()}",
                ChartName = $"Top Pages ({timePeriod.ToString()})",
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key} ({item.Value})", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView(DISPLAY_CHART_VIEW, model);
        }

        [ChildActionOnly]
        public ActionResult TopPosts(ChartSize chartSize, TimePeriod timePeriod)
        {
            var dataSet = _analyticsService.GetTopPosts(DetermineTimePeriod(timePeriod));

            var model = new ChartViewModel
            {
                ChartId = $"chart-top-posts-{timePeriod.ToString().ToLower()}",
                ChartName = $"Top Posts ({timePeriod.ToString()})",
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key} ({item.Value})", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView(DISPLAY_CHART_VIEW, model);
        }

        [ChildActionOnly]
        public ActionResult TopPostCategories(ChartSize chartSize, TimePeriod timePeriod)
        {
            var dataSet = _analyticsService.GetTopPostCategories(DetermineTimePeriod(timePeriod));

            var model = new ChartViewModel
            {
                ChartId = $"chart-top-post-categories-{timePeriod.ToString().ToLower()}",
                ChartName = $"Top Post Categories ({timePeriod.ToString()})",
                ChartSize = chartSize,
                ChartType = ChartType.Pie,
                ChartColumns = new List<ColumnViewModel>()
            };

            foreach (var item in dataSet)
            {
                model.ChartColumns.Add(new ColumnViewModel { ColumnName = $"{item.Key} ({item.Value})", ColumnValues = new List<int> { item.Value } });
            }

            return PartialView("_DisplayChart", model);
        }

        private static DateTime? DetermineTimePeriod(TimePeriod timePeriod)
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