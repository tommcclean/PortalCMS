using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.Helpers;

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

        public ActionResult Index(int pageId = 0)
        {
            if (pageId == 0)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            EvaluateSingleSignOn();

            var currentPage = _pageService.View(UserHelper.UserId, pageId);

            if (currentPage == null)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            return View("/Areas/PageBuilder/Views/Page/Index.cshtml", currentPage);
        }

        public async Task<ActionResult> Analytic(int pageId, string referrer)
        {
            var page = _pageService.Get(pageId);

            if (UserHelper.IsLoggedIn)
                await _analyticService.LogPageViewAsync(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.UserId);
            else
                await _analyticService.LogPageViewAsync(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }

        private void EvaluateSingleSignOn()
        {
            var resetCookie = Request.Cookies["PortalCMS_SSO"];

            if (!UserHelper.IsLoggedIn && resetCookie != null)
            {
                var cookieValues = resetCookie.Value.Split(',');

                var result = _loginService.SSO(Convert.ToInt32(cookieValues[0]), cookieValues[2]);

                if (result.HasValue)
                {
                    Session.Add("UserAccount", _userService.GetUser(result.Value));
                    Session.Add("UserRoles", _roleService.Get(result));
                }

                resetCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(resetCookie);
            }
        }
    }
}