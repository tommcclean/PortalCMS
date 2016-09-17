using Portal.CMS.Entities.Entities.Themes;
using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Builder.ViewModels.Build;
using Portal.CMS.Web.Areas.Builder.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class BuildController : Controller
    {
        #region Dependencies

        private readonly IPageService _pageService;
        private readonly IPageSectionService _pageSectionService;
        private readonly IPageSectionTypeService _pageSectionTypeService;
        private readonly IImageService _imageService;
        private readonly IAnalyticsService _analyticService;
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IRoleService _roleService;
        private readonly IThemeService _themeService;

        public BuildController(IPageService pageService, IPageSectionService pageSectionService, IPageSectionTypeService pageSectionTypeService, IImageService imageService, IAnalyticsService analyticService, IUserService userService, ILoginService loginService, IRoleService roleService, IThemeService themeService)
        {
            _pageService = pageService;
            _pageSectionService = pageSectionService;
            _pageSectionTypeService = pageSectionTypeService;
            _imageService = imageService;
            _analyticService = analyticService;
            _userService = userService;
            _loginService = loginService;
            _roleService = roleService;
            _themeService = themeService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index(int pageId)
        {
            var resetCookie = Request.Cookies["PortalCMS_SSO"];

            if (!UserHelper.IsLoggedIn && resetCookie != null)
            {
                var cookieValues = resetCookie.Value.Split(',');

                var result = _loginService.SSO(Convert.ToInt32(cookieValues[0]), cookieValues[2]);

                if (result.HasValue)
                {
                    Session.Add("UserAccount", _userService.GetUser(result.Value));
                    Session.Add("UserRoles", _roleService.Get(result.Value));
                }

                resetCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(resetCookie);
            }

            var model = new CustomViewModel()
            {
                Page = _pageService.View(UserHelper.UserId, pageId)
            };

            if (model.Page == null)
                return RedirectToAction("Index", "Home", new { area = "" });

            return View("/Areas/Builder/Views/Build/Index.cshtml", model);
        }

        public ActionResult Analytic(int pageId, string referrer)
        {
            var page = _pageService.Get(pageId);

            if (UserHelper.IsLoggedIn)
                _analyticService.LogPageView(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.UserId);
            else
                _analyticService.LogPageView(page.PageArea, page.PageController, page.PageAction, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }

        [HttpPost, AdminFilter]
        public ActionResult Order(int pageId, string sectionList)
        {
            if (sectionList != null && sectionList.Length > 2)
                _pageService.Order(pageId, sectionList);

            return RedirectToAction("Index", "Build", new { pageId = pageId });
        }

        [HttpPost]
        public ActionResult Contact(int pageSectionId, string yourName, string yourEmail, string yourSubject, string yourMessage)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            EmailHelper.Send(
                _userService.Get(new List<string> { "Admin" }).Select(x => x.EmailAddress).ToList(),
                "Contact Submitted",
                string.Format("<p>Hello, we thought you might like to know that a visitor to your website has submitted a message, here are the details we recorded.</p><p>Name: {0}</p><p>Email Address: {1}</p><p>Subject: {2}</p><p>Message: {3}</p>", yourName, yourEmail, yourSubject, yourMessage));

            return RedirectToAction("Index", new { pageId = pageSection.PageId });
        }

        [HttpGet, AdminFilter]
        public ActionResult Pages()
        {
            var model = new PagesViewModel
            {
                PageList = _pageService.Get().ToList()
            };

            return PartialView("_PageManager", model);
        }

        [HttpGet, AdminFilter]
        public ActionResult Themes(int pageId)
        {
            var model = new ThemeManagerViewModel
            {
                PageId = pageId,
                Themes = _themeService.Get(),
                Fonts = new List<Font>()
            };

            model.Fonts.AddRange(model.Themes.Select(x => x.TextFont));
            model.Fonts.AddRange(model.Themes.Select(x => x.TitleFont));

            return View(model);
        }
    }
}