using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Component;
using Portal.CMS.Web.ViewModels.Shared;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class ComponentController : Controller
    {
        #region Dependencies

        private readonly IPageSectionService _pageSectionService;
        private readonly IPageComponentService _pageComponentService;
        private readonly IImageService _imageService;

        private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";

        public ComponentController(IPageSectionService pageSectionService, IPageComponentService pageComponentService, IImageService imageService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentService = pageComponentService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Add()
        {
            var model = new AddViewModel
            {
                PageComponentTypeList = _pageComponentService.GetComponentTypes()
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Add(int pageSectionId, string containerElementId, string elementBody)
        {
            elementBody = elementBody.Replace("animated bounce", string.Empty);

            _pageComponentService.Add(pageSectionId, containerElementId, elementBody);

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
            _pageComponentService.EditElement(pageSectionId, elementId, elementHtml);

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Link(int pageSectionId, string elementId, string elementHtml, string elementHref, string elementTarget)
        {
            _pageComponentService.EditAnchor(pageSectionId, elementId, elementHtml, elementHref, elementTarget);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Image(int pageSectionId, string elementId, string elementType)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var imageList = _imageService.Get();

            var model = new ImageViewModel
            {
                SectionId = pageSectionId,
                ElementType = elementType,
                ElementId = elementId,
                GeneralImages = new PaginationViewModel
                {
                    PaginationType = "general",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.General)
                },
                IconImages = new PaginationViewModel
                {
                    PaginationType = "icon",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.Icon)
                },
                ScreenshotImages = new PaginationViewModel
                {
                    PaginationType = "screenshot",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.Screenshot)
                },
                TextureImages = new PaginationViewModel
                {
                    PaginationType = "texture",
                    TargetInputField = "SelectedImageId",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.Texture)
                }
            };

            return View("_Image", model);
        }

        [HttpPost]
        public JsonResult Image(ImageViewModel model)
        {
            try
            {
                _pageComponentService.EditImage(model.SectionId, model.ElementType, model.ElementId, model.SelectedImageId);

                var selectedImage = _imageService.Get(model.SelectedImageId);

                return Json(new { State = true, Source = selectedImage.ImagePath });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public ActionResult Video(int pageSectionId, string widgetWrapperElementId, string videoPlayerElementId)
        {
            var model = new VideoViewModel
            {
                SectionId = pageSectionId,
                WidgetWrapperElementId = widgetWrapperElementId,
                VideoPlayerElementId = videoPlayerElementId,
                VideoUrl = string.Empty
            };

            return View("_Video", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Video(VideoViewModel model)
        {
            try
            {
                _pageComponentService.EditSource(model.SectionId, model.VideoPlayerElementId, model.VideoUrl);

                return Json(new { State = true });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Freestyle(int pageSectionId, string elementId, string elementHtml)
        {
            // REPLACE: MCE Tokens
            elementHtml = elementHtml.Replace("ui-draggable ui-draggable-handle mce-content-body", string.Empty);
            elementHtml = elementHtml.Replace("contenteditable=\"true\" spellcheck=\"false\"", string.Empty);

            _pageComponentService.EditElement(pageSectionId, elementId, elementHtml);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Container(int pageSectionId, string elementId)
        {
            var model = new ContainerViewModel
            {
                SectionId = pageSectionId,
                ElementId = elementId
            };

            return View("_Container", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Container(ContainerViewModel model)
        {
            _pageSectionService.EditAnimation(model.SectionId, model.ElementId, model.Animation.ToString());

            return Content("Refresh");
        }

        private string SaveImage(HttpPostedFileBase imageFile)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToUpper();

            if (extension != ".PNG" && extension != ".JPG" && extension != ".GIF")
                throw new ArgumentException("Unexpected Image Format Provided");

            var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var imageFileName = $"media-{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{imageFile.FileName}";
            var path = Path.Combine(Server.MapPath(IMAGE_DIRECTORY), imageFileName);

            imageFile.SaveAs(path);

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace("Builder/Component/Image", string.Empty);
            var relativeFilePath = $"{siteURL}{IMAGE_DIRECTORY}/{imageFileName}";

            return relativeFilePath;
        }
    }
}