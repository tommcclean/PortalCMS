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
    }
}
