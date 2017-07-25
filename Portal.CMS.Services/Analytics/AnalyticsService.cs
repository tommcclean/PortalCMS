using LogBook.Services;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Analytics
{
    public interface IAnalyticsService
    {
        Task LogPageView(string area, string controller, string action, string referredUrl, string ipAddress, string userAgent, int? userId);

        Task LogPostView(int postId, string referredUrl, string ipAddress, string userAgent, int? UserId);

        List<KeyValuePair<string, int>> TotalHitsToday();

        List<KeyValuePair<string, int>> ErrorPercentage(DateTime sinceDate);

        List<KeyValuePair<string, int>> TotalHitsThisWeek();

        List<KeyValuePair<string, int>> TotalHitsThisMonth();

        List<KeyValuePair<string, int>> GetTopPages(DateTime? earliest);

        List<KeyValuePair<string, int>> GetTopPosts(DateTime? earliest);

        List<KeyValuePair<string, int>> GetTopPostCategories(DateTime? earliest);
    }

    public class AnalyticsService : IAnalyticsService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public AnalyticsService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task LogPageView(string area, string controller, string action, string referredUrl, string ipAddress, string userAgent, int? userId)
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

        public async Task LogPostView(int postId, string referredUrl, string ipAddress, string userAgent, int? UserId)
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

        public List<KeyValuePair<string, int>> TotalHitsToday()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = _context.AnalyticPageViews.ToList();
            var analyticPostViews = _context.AnalyticPostViews.ToList();

            var pageViewsToday = analyticPageViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month && x.DateAdded.Day == DateTime.Now.Day);
            var postViewsToday = analyticPostViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month && x.DateAdded.Day == DateTime.Now.Day);

            results.Add(new KeyValuePair<string, int>("Today", (pageViewsToday + postViewsToday)));

            var pageViewsMonth = analyticPageViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month);
            var postViewsMonth = analyticPostViews.Count(x => x.DateAdded.Year == DateTime.Now.Year && x.DateAdded.Month == DateTime.Now.Month);

            results.Add(new KeyValuePair<string, int>("This Month", (pageViewsMonth + postViewsMonth)));

            return results;
        }

        public List<KeyValuePair<string, int>> ErrorPercentage(DateTime sinceDate)
        {
            var results = new List<KeyValuePair<string, int>>();

            var pageViews = _context.AnalyticPageViews.Count(x => x.DateAdded >= sinceDate);
            var postViews = _context.AnalyticPostViews.Count(x => x.DateAdded >= sinceDate);
            results.Add(new KeyValuePair<string, int>("Total Hits", (pageViews + postViews)));

            var logHandler = new LogHandler();
            var errorCount = logHandler.ErrorsSinceTime(sinceDate);
            var errorPercentage = Math.Round((Convert.ToDouble(errorCount) / (Convert.ToDouble(postViews) + Convert.ToDouble(pageViews))) * 100, 2);
            results.Add(new KeyValuePair<string, int>($"Error Count ({errorPercentage}%)", errorCount));

            return results;
        }

        public List<KeyValuePair<string, int>> TotalHitsThisWeek()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = _context.AnalyticPageViews.ToList();
            var analyticPostViews = _context.AnalyticPostViews.ToList();

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

        public List<KeyValuePair<string, int>> TotalHitsThisMonth()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = _context.AnalyticPageViews.ToList();
            var analyticPostViews = _context.AnalyticPostViews.ToList();

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

        public List<KeyValuePair<string, int>> GetTopPages(DateTime? earliest)
        {
            var customPages = _context.Pages.ToList();
            var analyticPageViews = _context.AnalyticPageViews.Where(x => !earliest.HasValue || x.DateAdded > earliest.Value).ToList();
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

        public List<KeyValuePair<string, int>> GetTopPosts(DateTime? earliest)
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = _context.AnalyticPostViews.Where(x => !earliest.HasValue || x.DateAdded > earliest.Value).ToList();

            foreach (var post in _context.Posts.ToList())
            {
                var pageViews = analyticPostViews.Count(x => x.PostId == post.PostId);

                results.Add(new KeyValuePair<string, int>(post.PostTitle, pageViews));
            }

            return PruneAndOrder(results);
        }

        public List<KeyValuePair<string, int>> GetTopPostCategories(DateTime? earliest)
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = _context.AnalyticPostViews.Where(x => !earliest.HasValue || x.DateAdded > earliest.Value).ToList();

            foreach (var postCategory in _context.PostCategories.ToList())
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