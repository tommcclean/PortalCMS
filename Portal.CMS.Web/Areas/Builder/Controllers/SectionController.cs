using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Section;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class SectionController : Controller
    {
        #region Dependencies

        private readonly PageSectionService _pageSectionService;
        private readonly PageSectionTypeService _pageSectionTypeService;
        private readonly IImageService _imageService;

        public SectionController(PageSectionService pageSectionService, PageSectionTypeService pageSectionTypeService, ImageService imageService)
        {
            _pageSectionService = pageSectionService;
            _pageSectionTypeService = pageSectionTypeService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Edit(int sectionId)
        {
            var pageSection = _pageSectionService.Get(sectionId);

            var model = new EditViewModel()
            {
                PageId = pageSection.PageId,
                SectionId = sectionId,
                ImageList = _imageService.Get(),
                PageSectionHeight = _pageSectionService.DetermineSectionHeight(sectionId),
                PageSectionBackgroundType = _pageSectionService.DetermineBackgroundType(sectionId)
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (model.BackgroundImageId > 0)
                _pageSectionService.Background(model.SectionId, model.BackgroundImageId);

            _pageSectionService.Height(model.SectionId, model.PageSectionHeight);
            _pageSectionService.BackgroundType(model.SectionId, model.PageSectionBackgroundType);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Add(int pageId)
        {
            var model = new AddViewModel()
            {
                PageId = pageId,
                SectionTypeList = _pageSectionTypeService.Get()
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            _pageSectionService.Add(model.PageId, model.PageSectionTypeId);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Markup(int pageSectionId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var model = new MarkupViewModel()
            {
                PageSectionId = pageSectionId,
                PageSectionBody = pageSection.PageSectionBody
            };

            return View("_Markup", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Markup(MarkupViewModel model)
        {
            _pageSectionService.Markup(model.PageSectionId, model.PageSectionBody);

            return Content("Refresh");
        }
    }
}