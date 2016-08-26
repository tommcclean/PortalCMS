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
        public ActionResult Add()
        {
            var model = new AddViewModel
            {
                PageComponentTypeList = _pageComponentTypeService.Get(),
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Add(int pageSectionId, string containerElementId, string elementBody)
        {
            _pageComponentTypeService.Add(pageSectionId, containerElementId, elementBody);

            return Json(new { State = true });
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
        public ActionResult Image(int pageSectionId, string elementId, string elementType)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var model = new ImageViewModel
            {
                PageId = pageSection.PageId,
                SectionId = pageSectionId,
                ElementType = elementType,
                ElementId = elementId,
                ImageList = _imageService.Get()
            };

            return View("_Image", model);
        }

        [HttpPost]
        public ActionResult Image(ImageViewModel model)
        {
            _pageComponentService.EditImage(model.SectionId, model.ElementType, model.ElementId, model.SelectedImageId);

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Freestyle(int pageSectionId, string elementId, string elementHtml)
        {
            // REPLACE: MCE Tokens
            elementHtml = elementHtml.Replace("ui-draggable ui-draggable-handle mce-content-body", string.Empty);
            elementHtml = elementHtml.Replace("contenteditable=\"true\" spellcheck=\"false\"", string.Empty);

            _pageComponentService.Element(pageSectionId, elementId, elementHtml);

            return Content("Refresh");
        }

    }
}