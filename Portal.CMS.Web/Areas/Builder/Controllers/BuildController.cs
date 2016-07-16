using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Shared;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Builder.ViewModels.Build;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class BuildController : Controller
    {
        #region Dependencies

        private readonly PageService _pageService;
        private readonly PageSectionService _pageSectionService;
        private readonly PageSectionTypeService _pageSectionTypeService;
        private readonly IImageService _imageService;
        private readonly AnalyticsService _analyticService;

        public BuildController(PageService pageService, PageSectionService pageSectionService, PageSectionTypeService pageSectionTypeService, ImageService imageService, AnalyticsService analyticService)
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
                _analyticService.AnalysePageView(page.PageArea, page.PageController, page.PageAction, referrer, UserHelper.UserId);
            else
                _analyticService.AnalysePageView(page.PageArea, page.PageController, page.PageAction, referrer, null);

            return Json(new { State = true });
        }

        [HttpPost, AdminFilter]
        public ActionResult Order(int pageId, string sectionList)
        {
            //if (sectionList != null && sectionList.Length > 2)
            //    _pageService.Order(UserHelper.UserId, pageId, sectionList);

            return RedirectToAction("Edit", "Pages", new { id = pageId });
        }
    }
}