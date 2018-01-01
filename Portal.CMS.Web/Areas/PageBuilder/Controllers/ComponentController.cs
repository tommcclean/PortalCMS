using Portal.CMS.Entities.Enumerators;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Extensions;
using Portal.CMS.Web.Areas.PageBuilder.ViewModels.Component;
using Portal.CMS.Web.ViewModels.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [AdminFilter(ActionFilterResponseType.Modal)]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ComponentController : Controller
    {
        private readonly IPageSectionService _pageSectionService;
        private readonly IPageComponentService _pageComponentService;
        private readonly IImageService _imageService;

        public ComponentController(IPageSectionService pageSectionService, IPageComponentService pageComponentService, IImageService imageService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentService = pageComponentService;
            _imageService = imageService;
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public async Task<ActionResult> Add()
        {
            var model = new AddViewModel
            {
                PageComponentTypeList = await _pageComponentService.GetComponentTypesAsync()
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Add(int pageSectionId, string containerElementId, string elementBody)
        {
            elementBody = elementBody.Replace("animated bounce", string.Empty);

            await _pageComponentService.AddAsync(pageSectionId, containerElementId, elementBody);

            return Json(new { State = true });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int pageSectionId, string elementId)
        {
            try
            {
                await _pageComponentService.DeleteAsync(pageSectionId, elementId);

                return Json(new { State = true });
            }
            catch (Exception ex)
            {
                return Json(new { State = false, Message = ex.InnerException });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(int pageSectionId, string elementId, string elementHtml)
        {
            await _pageComponentService.EditElementAsync(pageSectionId, elementId, elementHtml);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> EditImage(int pageSectionId, string elementId, string elementType)
        {
            var imageList = await _imageService.GetAsync();

            var model = new ImageViewModel
            {
                SectionId = pageSectionId,
                ElementType = elementType,
                ElementId = elementId,
                GeneralImages = new PaginationViewModel
                {
                    PaginationType = "general",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General)
                },
                IconImages = new PaginationViewModel
                {
                    PaginationType = "icon",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.Icon)
                },
                ScreenshotImages = new PaginationViewModel
                {
                    PaginationType = "screenshot",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.Screenshot)
                },
                TextureImages = new PaginationViewModel
                {
                    PaginationType = "texture",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.Texture)
                }
            };

            return View("_EditImage", model);
        }

        [HttpPost]
        public async Task<JsonResult> EditImage(ImageViewModel model)
        {
            try
            {
                var selectedImage = await _imageService.GetAsync(model.SelectedImageId);

                await _pageComponentService.EditImageAsync(model.SectionId, model.ElementType, model.ElementId, selectedImage.CDNImagePath());

                return Json(new { State = true, Source = selectedImage.CDNImagePath() });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public ActionResult EditVideo(int pageSectionId, string widgetWrapperElementId, string videoPlayerElementId)
        {
            var model = new VideoViewModel
            {
                SectionId = pageSectionId,
                WidgetWrapperElementId = widgetWrapperElementId,
                VideoPlayerElementId = videoPlayerElementId,
                VideoUrl = string.Empty
            };

            return View("_EditVideo", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVideo(VideoViewModel model)
        {
            try
            {
                await _pageComponentService.EditSourceAsync(model.SectionId, model.VideoPlayerElementId, model.VideoUrl);

                return Json(new { State = true });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public ActionResult EditContainer(int pageSectionId, string elementId)
        {
            var model = new ContainerViewModel
            {
                SectionId = pageSectionId,
                ElementId = elementId
            };

            return View("_EditContainer", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditContainer(ContainerViewModel model)
        {
            await _pageSectionService.EditAnimationAsync(model.SectionId, model.ElementId, model.Animation.ToString());

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Link(int pageSectionId, string elementId, string elementHtml, string elementHref, string elementTarget)
        {
            await _pageComponentService.EditAnchorAsync(pageSectionId, elementId, elementHtml, elementHref, elementTarget);

            return Content("Refresh");
        }

        [HttpPost]
        public async Task<JsonResult> Clone(int pageSectionId, string elementId, string componentStamp)
        {
            await _pageComponentService.CloneElementAsync(pageSectionId, elementId, componentStamp);

            return Json(new { State = true });
        }
    }
}