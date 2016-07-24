using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Component;
using System;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class ComponentController : Controller
    {
        #region Dependencies

        private readonly IPageSectionService _pageSectionService;
        private readonly IPageComponentTypeService _pageComponentTypeService;
        private readonly IPageComponentService _pageComponentService;
        private readonly IImageService _imageService;

        public ComponentController(IPageSectionService pageSectionService, IPageComponentTypeService pageComponentTypeService, IPageComponentService pageComponentService, IImageService imageService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentTypeService = pageComponentTypeService;
            _pageComponentService = pageComponentService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Add(int pageSectionId, string elementId)
        {
            var model = new AddViewModel()
            {
                PageSectionId = pageSectionId,
                PageComponentTypeList = _pageComponentTypeService.Get(),
                ContainerElementId = elementId
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            _pageComponentTypeService.Add(model.PageSectionId, model.ContainerElementId, model.PageComponentTypeId);

            return Content("Refresh");
        }

        [HttpPost]
        public ActionResult Delete(int pageSectionId, string elementId)
        {
            try
            {
                _pageComponentService.Delete(pageSectionId, elementId);

                return Json(new { State = true });
            }
            catch (Exception ex)
            {
                return Json(new { State = false, Message = ex.InnerException });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int pageSectionId, string elementId, string elementHtml)
        {
            _pageComponentService.Element(pageSectionId, elementId, elementHtml);

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Link(int pageSectionId, string elementId, string elementHtml, string elementHref, string elementTarget)
        {
            _pageComponentService.Anchor(pageSectionId, elementId, elementHtml, elementHref, elementTarget);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Image(int pageSectionId, string elementId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var model = new ImageViewModel
            {
                PageId = pageSection.PageId,
                SectionId = pageSectionId,
                ElementId = elementId,
                ImageList = _imageService.Get()
            };

            return View("_Image", model);
        }

        [HttpPost]
        public ActionResult Image(ImageViewModel model)
        {
            _pageComponentService.EditImage(model.SectionId, model.ElementId, model.SelectedImageId);

            return Content("Refresh");
        }
    }
}