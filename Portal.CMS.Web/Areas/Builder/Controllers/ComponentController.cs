using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Component;
using Portal.CMS.Web.Areas.Builder.ViewModels.Shared;
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

        readonly IPageSectionService _pageSectionService;
        readonly IPageComponentTypeService _pageComponentTypeService;
        readonly IPageComponentService _pageComponentService;
        readonly IImageService _imageService;

        private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";

        public ComponentController(IPageSectionService pageSectionService, IPageComponentTypeService pageComponentTypeService, IPageComponentService pageComponentService, IImageService imageService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentTypeService = pageComponentTypeService;
            _pageComponentService = pageComponentService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        [OutputCache(Duration = 86400)]
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

            var imageList = _imageService.Get();

            var model = new ImageViewModel
            {
                PageId = pageSection.PageId,
                SectionId = pageSectionId,
                ElementType = elementType,
                ElementId = elementId,
                GeneralImages = new PaginationViewModel
                {
                    PaginationType = "general",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.General),
                },
                IconImages = new PaginationViewModel
                {
                    PaginationType = "icon",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.Icon),
                },
                ScreenshotImages = new PaginationViewModel
                {
                    PaginationType = "screenshot",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.Screenshot),
                },
                TextureImages = new PaginationViewModel
                {
                    PaginationType = "texture",
                    ImageList = imageList.Where(x => x.ImageCategory == Entities.Entities.Generic.ImageCategory.Texture),
                }
            };

            model.GeneralImages.PageCount = Math.Ceiling(Convert.ToDouble(model.GeneralImages.ImageList.Count()) / 8);
            model.IconImages.PageCount = Math.Ceiling(Convert.ToDouble(model.IconImages.ImageList.Count()) / 8);
            model.ScreenshotImages.PageCount = Math.Ceiling(Convert.ToDouble(model.ScreenshotImages.ImageList.Count()) / 8);
            model.TextureImages.PageCount = Math.Ceiling(Convert.ToDouble(model.TextureImages.ImageList.Count()) / 8);

            return View("_Image", model);
        }

        [HttpPost]
        public ActionResult Image(ImageViewModel model)
        {
            var selectedImageId = model.SelectedImageId;

            if (model.AttachedImage != null)
            {
                var imageFilePath = SaveImage(model.AttachedImage);

                selectedImageId = _imageService.Create(imageFilePath, model.ImageCategory);
            }

            _pageComponentService.EditImage(model.SectionId, model.ElementType, model.ElementId, selectedImageId);

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

        private string SaveImage(HttpPostedFileBase imageFile)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToUpper();

            if (extension != ".PNG" && extension != ".JPG" && extension != ".GIF")
                throw new ArgumentException("Unexpected Image Format Provided");

            var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var imageFileName = string.Format("media-{0}-{1}", DateTime.Now.ToString("ddMMyyyyHHmmss"), imageFile.FileName);
            var path = Path.Combine(Server.MapPath(IMAGE_DIRECTORY), imageFileName);

            imageFile.SaveAs(path);

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace("Builder/Component/Image", string.Empty);
            var relativeFilePath = string.Format("{0}{1}/{2}", siteURL, IMAGE_DIRECTORY, imageFileName);

            return relativeFilePath;
        }
    }
}