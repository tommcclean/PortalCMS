using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Analytics
{
    public interface IAnalyticsService
    {
        void LogPageView(string area, string controller, string action, string referredUrl, string ipAddress, string userAgent, int? userId);

        void LogPostView(int postId, string referredUrl, string ipAddress, string userAgent, int? UserId);

        List<KeyValuePair<string, int>> TotalHitsThisWeek();

        List<KeyValuePair<string, int>> GetTopPages();

        List<KeyValuePair<string, int>> GetTopPosts();

        List<KeyValuePair<string, int>> GetTopPostCategories();
    }

    public class AnalyticsService : IAnalyticsService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        #endregion Dependencies

        public void LogPageView(string area, string controller, string action, string referredUrl, string ipAddress, string userAgent, int? userId)
        {
            var newAnalyticPageView = new AnalyticPageView()
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

            _context.SaveChanges();
        }

        public void LogPostView(int postId, string referredUrl, string ipAddress, string userAgent, int? UserId)
        {
            var analyticPostView = new AnalyticPostView()
            {
                PostId = postId,
                ReferredUrl = referredUrl,
                IPAddress = ipAddress,
                UserAgent = userAgent,
                UserId = UserId ?? 0,
                DateAdded = DateTime.Now
            };

            _context.AnalyticPostViews.Add(analyticPostView);

            _context.SaveChanges();
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

                var label = (date == DateTime.Now ? date.DayOfWeek.ToString() + " (Today)" : date.DayOfWeek.ToString());

                results.Add(new KeyValuePair<string, int>(label, (pageViews + postViews)));
            }

            return results;
        }

        public List<KeyValuePair<string, int>> GetTopPages()
        {
            var results = new List<KeyValuePair<string, int>>();

            var customPages = _context.Pages.ToList();

            var analyticPageViews = _context.AnalyticPageViews.ToList();

            var analyticPages = analyticPageViews.GroupBy(page => new { page.Area, page.Controller, page.Action });

            foreach(var page in analyticPages)
            {
                var customPage = customPages.FirstOrDefault(x => (x.PageArea ?? "") == page.Key.Area && x.PageController == page.Key.Controller && x.PageAction == page.Key.Action);

                if (customPage == null)
                    results.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}/{2}", page.Key.Area, page.Key.Controller, page.Key.Action), page.Count()));
                else
                    results.Add(new KeyValuePair<string, int>(customPage.PageName, page.Count()));
            }

            PruneAndOrder(results);

            return results;
        }

        public List<KeyValuePair<string, int>> GetTopPosts()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = _context.AnalyticPostViews.ToList();

            foreach (var post in _context.Posts.ToList())
            {
                var pageViews = analyticPostViews.Count(x => x.PostId == post.PostId);

                results.Add(new KeyValuePair<string, int>(post.PostTitle, pageViews));
            }

            PruneAndOrder(results);

            return results;
        }

        public List<KeyValuePair<string, int>> GetTopPostCategories()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = _context.AnalyticPostViews.ToList();

            foreach (var postCategory in _context.PostCategories.ToList())
            {
                int pageViews = 0;

                foreach (var post in postCategory.Posts)
                {
                    pageViews += analyticPostViews.Count(x => x.PostId == post.PostId);
                }

                results.Add(new KeyValuePair<string, int>(postCategory.PostCategoryName, pageViews));
            }

            PruneAndOrder(results);

            return results;
        }

        private void PruneAndOrder(List<KeyValuePair<string, int>> results)
        {
            // REMOVE: Empty Items
            results = results.Where(x => x.Value > 0).ToList();

            // TAKE: Top 5 Results
            results = results.OrderByDescending(x => x.Value).Take(5).ToList();
        }
    }
}