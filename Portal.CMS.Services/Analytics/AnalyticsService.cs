using LogBook.Services;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Analytics
{
    public interface IAnalyticsService
    {
        Task LogPageViewAsync(string area, string controller, string action, string referredUrl, string ipAddress, string userAgent, int? userId);

        Task LogPostViewAsync(int postId, string referredUrl, string ipAddress, string userAgent, int? UserId);

        Task<List<KeyValuePair<string, int>>> TotalHitsTodayAsync();

        Task<List<KeyValuePair<string, int>>> ErrorPercentageAsync(DateTime sinceDate);

        Task<List<KeyValuePair<string, int>>> TotalHitsThisWeekAsync();

        Task<List<KeyValuePair<string, int>>> TotalHitsThisMonthAsync();

        Task<List<KeyValuePair<string, int>>> GetTopPagesAsync(DateTime? earliest);

        Task<List<KeyValuePair<string, int>>> GetTopPostsAsync(DateTime? earliest);

        Task<List<KeyValuePair<string, int>>> GetTopPostCategoriesAsync(DateTime? earliest);
    }

    public class AnalyticsService : IAnalyticsService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public AnalyticsService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task LogPageViewAsync(string area, string controller, string action, string referredUrl, string ipAddress, string userAgent, int? userId)
        {
            var newAnalyticPageView = new AnalyticPageView
            {
                Area = area ?? "",
                Controller = controller,
                Action = action,
                ReferredUrl = referredUrl,
                IPAddress = ipAddress,
                UserAgent = userAgent,
                UserId = userId ?? 0,
                DateAdded = DateTime.Now
            };

            _context.AnalyticPageViews.Add(newAnalyticPageView);

            await _context.SaveChangesAsync();
        }

        public async Task LogPostViewAsync(int postId, string referredUrl, string ipAddress, string userAgent, int? UserId)
        {
            var analyticPostView = new AnalyticPostView
            {
                PostId = postId,
                ReferredUrl = referredUrl,
                IPAddress = ipAddress,
                UserAgent = userAgent,
                UserId = UserId ?? 0,
                DateAdded = DateTime.Now
            };

            _context.AnalyticPostViews.Add(analyticPostView);

            await _context.SaveChangesAsync();
        }

        public async Task<List<KeyValuePair<string, int>>> TotalHitsTodayAsync()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = await _context.AnalyticPageViews.ToListAsync();
            var analyticPostViews = await _context.AnalyticPostViews.ToListAsync();

            var pageViewsToday = analyticPageViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month && x.DateAdded.Day == DateTime.Now.Day);
            var postViewsToday = analyticPostViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month && x.DateAdded.Day == DateTime.Now.Day);

            results.Add(new KeyValuePair<string, int>("Today", (pageViewsToday + postViewsToday)));

            var pageViewsMonth = analyticPageViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month);
            var postViewsMonth = analyticPostViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month);

            results.Add(new KeyValuePair<string, int>("This Month", (pageViewsMonth + postViewsMonth)));

            return results;
        }

        public async Task<List<KeyValuePair<string, int>>> ErrorPercentageAsync(DateTime sinceDate)
        {
            var results = new List<KeyValuePair<string, int>>();

            var pageViews = await _context.AnalyticPageViews.CountAsync(x => x.DateAdded >= sinceDate);
            var postViews = await _context.AnalyticPostViews.CountAsync(x => x.DateAdded >= sinceDate);
            results.Add(new KeyValuePair<string, int>("Total Hits", (pageViews + postViews)));

            var logHandler = new LogHandler();
            var errorCount = logHandler.ErrorsSinceTime(sinceDate);
            var errorPercentage = Math.Round((Convert.ToDouble(errorCount) / (Convert.ToDouble(postViews) + Convert.ToDouble(pageViews))) * 100, 2);
            results.Add(new KeyValuePair<string, int>($"Error Count ({errorPercentage}%)", errorCount));

            return results;
        }

        public async Task<List<KeyValuePair<string, int>>> TotalHitsThisWeekAsync()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = await _context.AnalyticPageViews.ToListAsync();
            var analyticPostViews = await _context.AnalyticPostViews.ToListAsync();

            for (int loop = 1; loop < 8; loop += 1)
            {
                var date = DateTime.Now.AddDays(-7).AddDays(loop);

                var pageViews = analyticPageViews.Count(x => x.DateAdded.Year == date.Year && x.DateAdded.Month == date.Month && x.DateAdded.Day == date.Day);
                var postViews = analyticPostViews.Count(x => x.DateAdded.Year == date.Year && x.DateAdded.Month == date.Month && x.DateAdded.Day == date.Day);

                var label = (date == DateTime.Now ? date.DayOfWeek + " (Today)" : date.DayOfWeek.ToString());

                results.Add(new KeyValuePair<string, int>(label, (pageViews + postViews)));
            }

            return results;
        }

        public async Task<List<KeyValuePair<string, int>>> TotalHitsThisMonthAsync()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = await _context.AnalyticPageViews.ToListAsync();
            var analyticPostViews = await _context.AnalyticPostViews.ToListAsync();

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int loop = 1; loop < daysInMonth; loop += 7)
            {
                var weekEarliest = new DateTime(DateTime.Now.Year, DateTime.Now.Month, loop);

                var lastDay = (loop + 6);

                if (lastDay > daysInMonth)
                    lastDay = daysInMonth;

                var weekLatest = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDay);

                var pageViews = analyticPageViews.Count(x => x.DateAdded > weekEarliest && x.DateAdded < weekLatest);
                var postViews = analyticPostViews.Count(x => x.DateAdded > weekEarliest && x.DateAdded < weekLatest);

                results.Add(new KeyValuePair<string, int>(string.Format("{0} to {1}", loop, lastDay), (pageViews + postViews)));
            }

            return results;
        }

        public async Task<List<KeyValuePair<string, int>>> GetTopPagesAsync(DateTime? earliest)
        {
            var customPages = await _context.Pages.ToListAsync();
            var analyticPageViews = await _context.AnalyticPageViews.Where(x => !earliest.HasValue || x.DateAdded > earliest.Value).ToListAsync();
            var analyticPages = analyticPageViews.GroupBy(page => new { page.Area, page.Controller, page.Action });

            var results = new List<KeyValuePair<string, int>>();

            foreach (var page in analyticPages)
            {
                var customPage = customPages.FirstOrDefault(x => (x.PageArea ?? "") == page.Key.Area && x.PageController == page.Key.Controller && x.PageAction == page.Key.Action);

                if (customPage == null)
                    results.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}/{2}", page.Key.Area, page.Key.Controller, page.Key.Action), page.Count()));
                else
                    results.Add(new KeyValuePair<string, int>(customPage.PageName, page.Count()));
            }

            return PruneAndOrder(results);
        }

        public async Task<List<KeyValuePair<string, int>>> GetTopPostsAsync(DateTime? earliest)
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = await _context.AnalyticPostViews.Where(x => !earliest.HasValue || x.DateAdded > earliest.Value).ToListAsync();

            foreach (var post in _context.Posts.ToList())
            {
                var pageViews = analyticPostViews.Count(x => x.PostId == post.PostId);

                results.Add(new KeyValuePair<string, int>(post.PostTitle, pageViews));
            }

            return PruneAndOrder(results);
        }

        public async Task<List<KeyValuePair<string, int>>> GetTopPostCategoriesAsync(DateTime? earliest)
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = await _context.AnalyticPostViews.Where(x => !earliest.HasValue || x.DateAdded > earliest.Value).ToListAsync();

            foreach (var postCategory in await _context.PostCategories.ToListAsync())
            {
                var pageViews = 0;

                foreach (var post in postCategory.Posts)
                {
                    pageViews += analyticPostViews.Count(x => x.PostId == post.PostId);
                }

                results.Add(new KeyValuePair<string, int>(postCategory.PostCategoryName, pageViews));
            }

            return PruneAndOrder(results);
        }

        private static List<KeyValuePair<string, int>> PruneAndOrder(List<KeyValuePair<string, int>> results)
        {
            // REMOVE: Empty Items
            results = results.Where(x => x.Value > 0).ToList();

            // TAKE: Top 5 Results
            results = results.OrderByDescending(x => x.Value).Take(5).ToList();

            return results;
        }
    }
}