using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.MediaManager;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class MediaManagerController : Controller
    {
        #region Dependencies

        private readonly IImageService _imageService;
        private readonly IFontService _fontService;
        private readonly IPostService _postService;
        private readonly IPostImageService _postImageService;

        private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";
        private const string FONT_DIRECTORY = "/Areas/Admin/Content/Fonts/Uploads";

        public MediaManagerController(IPostService postService, IPostImageService postImageService, IImageService imageService, IFontService fontService)
        {
            _postService = postService;
            _postImageService = postImageService;
            _imageService = imageService;
            _fontService = fontService;
        }

        #endregion Dependencies

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> Index()
        {
            var model = new MediaViewModel
            {
                Images = await _imageService.GetAsync(),
                Fonts = await _fontService.GetAsync()
            };

            return View(model);
        }

        [HttpGet, EditorFilter(ActionFilterResponseType.Modal)]
        [OutputCache(Duration = 86400)]
        public ActionResult UploadImage()
        {
            var model = new UploadImageViewModel();

            return PartialView("_UploadImage", model);
        }

        [HttpPost, EditorFilter(ActionFilterResponseType.Modal)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadImage(UploadImageViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_UploadImage", model);

            var imageFilePath = SaveImage(model.AttachedImage);

            await _imageService.CreateAsync(imageFilePath, model.ImageCategory);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        [OutputCache(Duration = 86400)]
        public ActionResult UploadFont()
        {
            var model = new UploadFontViewModel();

            return PartialView("_UploadFont", model);
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Modal)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadFont(UploadFontViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_UploadFont", model);

            var fontFilePath = SaveFont(model.AttachedFont);

            await _fontService.CreateAsync(model.FontName, model.FontType, fontFilePath);

            return Content("Refresh");
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> DeleteImage(int imageId)
        {
            var image = await _imageService.GetAsync(imageId);

            var fileNamePosition = image.ImagePath.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
            var fileName = image.ImagePath.Substring(fileNamePosition, image.ImagePath.Length - fileNamePosition);

            var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

            var imageFilePath = $@"{destinationDirectory}\{fileName}";

            if (System.IO.File.Exists(imageFilePath))
                System.IO.File.Delete(imageFilePath);

            await _imageService.DeleteAsync(imageId);

            return RedirectToAction(nameof(Index), "MediaManager");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> DeleteFont(int fontId)
        {
            var font = await _fontService.GetAsync(fontId);

            var fileNamePosition = font.FontPath.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
            var fileName = font.FontPath.Substring(fileNamePosition, font.FontPath.Length - fileNamePosition);

            var destinationDirectory = Path.Combine(Server.MapPath(FONT_DIRECTORY));

            var fontFilePath = $@"{destinationDirectory}\{fileName}";

            if (System.IO.File.Exists(fontFilePath))
                System.IO.File.Delete(fontFilePath);

            await _fontService.DeleteAsync(fontId);

            return RedirectToAction(nameof(Index), "MediaManager");
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

            var relativeFilePath = $"{IMAGE_DIRECTORY}/{imageFileName}";

            return relativeFilePath;
        }

        private string SaveFont(HttpPostedFileBase fontFile)
        {
            var extension = Path.GetExtension(fontFile.FileName).ToUpper();

            if (extension != ".OTF" && extension != ".TTF")
                throw new ArgumentException("Unexpected Font Format Provided");

            var destinationDirectory = Path.Combine(Server.MapPath(FONT_DIRECTORY));

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var fontFileName = $"font-{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{fontFile.FileName}";
            var path = Path.Combine(Server.MapPath(FONT_DIRECTORY), fontFileName);

            fontFile.SaveAs(path);

            var relativeFilePath = $"{FONT_DIRECTORY}/{fontFileName}";

            return relativeFilePath;
        }
    }
}