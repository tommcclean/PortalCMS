using PortalCMS.Services.Analytics;
using PortalCMS.Services.PageBuilder;
using PortalCMS.Web.Architecture.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PortalCMS.Web.Areas.PageBuilder.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IPageSectionService _sectionService;
        private readonly IAnalyticsService _analyticService;

        public PageController(IPageService pageService, IPageSectionService sectionService, IAnalyticsService analyticService)
        {
            _pageService = pageService;
            _sectionService = sectionService;
            _analyticService = analyticService;
        }

        public async Task<ActionResult> Index(int pageId = 0)
        {
            if (pageId == 0)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            var currentPage = await _pageService.ViewAsync(UserHelper.Id, pageId);

            if (currentPage == null)
            {
                return RedirectToAction(nameof(Index), "Home", new { area = "" });
            }

            if (UserHelper.IsEditor)
            {
                return View("/Areas/PageBuilder/Views/Page/Editor.cshtml", currentPage);
            }
            else
            {
                return View("/Areas/PageBuilder/Views/Page/Guest.cshtml", currentPage);
            }

            
        }

        public async Task<ActionResult> Analytic(int pageId, string referrer)
        {
            var page = await _pageService.GetAsync(pageId);

            if (UserHelper.IsLoggedIn)
                await _analyticService.LogPageViewAsync(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.Id);
            else
                await _analyticService.LogPageViewAsync(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }
    }
}