using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class PageController : Controller
    {
        #region Dependencies

        private readonly IPageService _pageService;
        private readonly IPageSectionService _sectionService;
        private readonly IAnalyticsService _analyticService;
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IRoleService _roleService;

        public PageController(IPageService pageService, IPageSectionService sectionService, IAnalyticsService analyticService, IUserService userService, ILoginService loginService, IRoleService roleService)
        {
            _pageService = pageService;
            _sectionService = sectionService;
            _analyticService = analyticService;
            _userService = userService;
            _loginService = loginService;
            _roleService = roleService;
        }

        #endregion Dependencies

        public async Task<ActionResult> Index(int pageId = 0)
        {
            if (pageId == 0)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            await EvaluateSingleSignOnAsync();

            var currentPage = await _pageService.ViewAsync(UserHelper.UserId, pageId);

            if (currentPage == null)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            return View("/Areas/PageBuilder/Views/Page/Index.cshtml", currentPage);
        }

        public async Task<ActionResult> Analytic(int pageId, string referrer)
        {
            var page = await _pageService.GetAsync(pageId);

            if (UserHelper.IsLoggedIn)
                await _analyticService.LogPageViewAsync(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.UserId);
            else
                await _analyticService.LogPageViewAsync(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }

        private async Task EvaluateSingleSignOnAsync()
        {
            var resetCookie = Request.Cookies["PortalCMS_SSO"];

            if (!UserHelper.IsLoggedIn && resetCookie != null)
            {
                var cookieValues = resetCookie.Value.Split(',');

                var result = await _loginService.SSOAsync(Convert.ToInt32(cookieValues[0]), cookieValues[2]);

                if (result.HasValue)
                {
                    Session.Add("UserAccount", await _userService.GetUserAsync(result.Value));
                    Session.Add("UserRoles", await _roleService.GetAsync(result));
                }

                resetCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(resetCookie);
            }
        }
    }
}