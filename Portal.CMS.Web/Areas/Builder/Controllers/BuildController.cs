using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Shared;
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
        public ActionResult Element(int sectionId, string elementId)
        {
            var pageSection = _pageSectionService.Get(sectionId);

            var document = new Document(pageSection.PageSectionBody);

            var model = new ElementViewModel()
            {
                PageId = pageSection.PageId,
                SectionId = sectionId,
                ElementId = elementId,
                ElementValue = document.GetContent(elementId)
            };

            return View("_Element", model);
        }

        [HttpPost, AdminFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Element(ElementViewModel model)
        {
            _pageSectionService.Element(model.SectionId, model.ElementId, model.ElementValue, model.ElementColour);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Delete(int pageId, int sectionId)
        {
            _pageSectionService.Delete(sectionId);

            return RedirectToAction("Index", "Build", new { pageId = pageId });
        }
    }
}