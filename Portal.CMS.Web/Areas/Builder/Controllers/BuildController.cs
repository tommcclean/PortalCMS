using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Builder.ViewModels.Build;
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

        public BuildController(IPageService pageService, IPageSectionService pageSectionService, IPageSectionTypeService pageSectionTypeService, IImageService imageService, IAnalyticsService analyticService)
        {
            _pageService = pageService;
            _pageSectionService = pageSectionService;
            _pageSectionTypeService = pageSectionTypeService;
            _imageService = imageService;
            _analyticService = analyticService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index(int pageId)
        {
            var model = new CustomViewModel()
            {
                Page = _pageService.Get(pageId)
            };

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
        public ActionResult Contact(int pageId, string senderName, string senderEmail, string senderSubject, string senderMessage)
        {
            return RedirectToAction("Index", new { pageId = pageId });
        }
    }
}