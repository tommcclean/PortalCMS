using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Analytics
{
    public class AnalyticsService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        #endregion Dependencies

        public void AnalysePageView(string area, string controller, string action, string referredUrl, int? userId)
        {
            var newAnalyticPageView = new AnalyticPageView()
            {
                Area = area ?? "",
                Controller = controller,
                Action = action,
                ReferredUrl = referredUrl,
                UserId = userId ?? 0,
                DateAdded = DateTime.Now
            };

            _context.AnalyticPageViews.Add(newAnalyticPageView);

            _context.SaveChanges();
        }

        public void AnalysePostView(int postId, string referredUrl, int? UserId)
        {
            var analyticPostView = new AnalyticPostView()
            {
                PostId = postId,
                ReferredUrl = referredUrl,
                UserId = UserId ?? 0,
                DateAdded = DateTime.Now
            };

            _context.AnalyticPostViews.Add(analyticPostView);

            _context.SaveChanges();
        }

        public List<KeyValuePair<string, int>> GetPageViewsByDay()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = _context.AnalyticPageViews.ToList();

            for(int loop = 1; loop < 8; loop += 1)
            {
                var date = DateTime.Now.AddDays(-7).AddDays(loop);

                var pageViews = analyticPageViews.Count(x => x.DateAdded.Year == date.Year && x.DateAdded.Month == date.Month && x.DateAdded.Day == date.Day);

                var label = (date == DateTime.Now ? date.DayOfWeek.ToString() + " (Today)" : date.DayOfWeek.ToString());

                results.Add(new KeyValuePair<string, int>(label, pageViews));
            }

            return results;
        }

        public List<KeyValuePair<string, int>> GetTopPages()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPageViews = _context.AnalyticPageViews.ToList();

            foreach(var page in _context.Pages.ToList())
            {
                var pageViews = analyticPageViews.Count(x => x.Area == (page.PageArea ?? "") && x.Controller == page.PageController && x.Action == page.PageAction);

                results.Add(new KeyValuePair<string, int>(page.PageName, pageViews));
            }

            // TAKE: 5 Top Posts
            results = results.OrderBy(x => x.Value).Take(5).ToList();

            return results.Where(x => x.Value > 0).OrderBy(x => x.Key).ToList();
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

            // TAKE: 5 Top Posts
            results = results.OrderBy(x => x.Value).Take(5).ToList();

            return results.Where(x => x.Value > 0).OrderBy(x => x.Key).ToList();
        }

        public List<KeyValuePair<string, int>> GetTopPostCategories()
        {
            var results = new List<KeyValuePair<string, int>>();

            var analyticPostViews = _context.AnalyticPostViews.ToList();

            foreach (var postCategory in _context.PostCategories.ToList())
            {
                int pageViews = 0;

                foreach(var post in postCategory.Posts)
                {
                    pageViews += analyticPostViews.Count(x => x.PostId == post.PostId);
                }

                results.Add(new KeyValuePair<string, int>(postCategory.PostCategoryName, pageViews));
            }

            // TAKE: 5 Top Posts
            results = results.OrderBy(x => x.Value).Take(5).ToList();

            return results.Where(x => x.Value > 0).OrderBy(x => x.Key).ToList();
        }
    }
}
