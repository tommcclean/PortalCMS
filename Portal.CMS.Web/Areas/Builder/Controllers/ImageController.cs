using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Image;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class ImageController : Controller
    {
        #region Dependencies

        private readonly PageSectionService _pageSectionService;
        private readonly PageComponentService _pageComponentService;
        private readonly IImageService _imageService;

        public ImageController(PageSectionService pageSectionService, PageComponentService pageComponentService, ImageService imageService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentService = pageComponentService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Edit(int pageSectionId, string elementId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var model = new EditViewModel()
            {
                PageId = pageSection.PageId,
                SectionId = pageSectionId,
                ElementId = elementId,
                ImageList = _imageService.Get()
            };

            return View("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            _pageComponentService.EditImage(model.SectionId, model.ElementId, model.SelectedImageId);

            return Content("Refresh");
        }
    }
}