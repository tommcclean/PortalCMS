using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
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

        public BuildController(PageService pageService, PageSectionService pageSectionService, PageSectionTypeService pageSectionTypeService, ImageService imageService)
        {
            _pageService = pageService;
            _pageSectionService = pageSectionService;
            _pageSectionTypeService = pageSectionTypeService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index(int pageId)
        {
            var model = new CustomViewModel()
            {
                Page = _pageService.Get(pageId)
            };

            return View("PageBuilder", model);
        }

        [HttpGet, AdminFilter]
        public ActionResult Add(int pageId)
        {
            var model = new AddViewModel()
            {
                PageId = pageId,
                SectionTypeList = _pageSectionTypeService.Get()
            };

            return View("_Add", model);
        }

        [HttpPost, AdminFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            _pageSectionService.Add(model.PageId, model.PageSectionTypeId);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Section(int sectionId)
        {
            var pageSection = _pageSectionService.Get(sectionId);

            var model = new SectionViewModel()
            {
                PageId = pageSection.PageId,
                SectionId = sectionId,
                ImageList = _imageService.Get(),
                PageSectionHeight = _pageSectionService.DetermineSectionHeight(sectionId),
                PageSectionBackgroundType = _pageSectionService.DetermineBackgroundType(sectionId)
            };

            return View("_Section", model);
        }

        [HttpPost, AdminFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Section(SectionViewModel model)
        {
            if (model.BackgroundImageId > 0)
                _pageSectionService.Background(model.SectionId, model.BackgroundImageId);

            _pageSectionService.Height(model.SectionId, model.PageSectionHeight);
            _pageSectionService.BackgroundType(model.SectionId, model.PageSectionBackgroundType);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Element(int sectionId, string elementId)
        {
            var pageSection = _pageSectionService.Get(sectionId);

            var model = new ElementViewModel()
            {
                PageId = pageSection.PageId,
                SectionId = sectionId,
                ElementId = elementId,
                ElementValue = _pageSectionService.Get(sectionId, elementId)
            };

            return View("_Element", model);
        }

        [HttpPost, AdminFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Element(ElementViewModel model)
        {
            _pageSectionService.Element(model.SectionId, model.ElementId, model.ElementValue, model.ElementColour);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Delete(int pageId, int sectionId)
        {
            _pageSectionService.Delete(sectionId);

            return RedirectToAction("Index", "Build", new { pageId = pageId });
        }
    }
}