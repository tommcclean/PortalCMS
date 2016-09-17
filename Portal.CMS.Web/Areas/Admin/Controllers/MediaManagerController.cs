using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.MediaManager;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter]
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

        [HttpGet, AdminFilter]
        public ActionResult Index()
        {
            var model = new MediaViewModel
            {
                Images = _imageService.Get(),
                Fonts = _fontService.Get()
            };

            return View(model);
        }

        [HttpGet, EditorFilter]
        public ActionResult UploadImage()
        {
            var model = new UploadImageViewModel();

            return PartialView("_UploadImage", model);
        }

        [HttpPost, EditorFilter]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(UploadImageViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_UploadImage", model);

            var imageFilePath = SaveImage(model.AttachedImage, nameof(UploadImage));

            var imageId = _imageService.Create(imageFilePath, model.ImageCategory);

            return this.Content("Refresh");
        }

        [HttpGet, EditorFilter]
        public ActionResult UploadFont()
        {
            var model = new UploadFontViewModel();

            return PartialView("_UploadFont", model);
        }

        [HttpPost, EditorFilter]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFont(UploadFontViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_UploadFont", model);

            var fontFilePath = SaveFont(model.AttachedFont, nameof(UploadFont));

            var fontId = _fontService.Create(model.FontName, model.FontType, fontFilePath);

            return this.Content("Refresh");
        }

        [HttpPost, AdminFilter]
        public ActionResult DeleteImage(int imageId)
        {
            var image = _imageService.Get(imageId);

            var fileNamePosition = image.ImagePath.LastIndexOf("/") + 1;
            var fileName = image.ImagePath.Substring(fileNamePosition, image.ImagePath.Length - fileNamePosition);

            var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

            var imageFilePath = string.Format(@"{0}\{1}", destinationDirectory, fileName);

            if (System.IO.File.Exists(imageFilePath))
                System.IO.File.Delete(imageFilePath);

            _imageService.Delete(imageId);

            return RedirectToAction(nameof(Index), "MediaManager");
        }

        [HttpGet, AdminFilter]
        public ActionResult DeleteFont(int fontId)
        {
            var font = _fontService.Get(fontId);

            var fileNamePosition = font.FontPath.LastIndexOf("/") + 1;
            var fileName = font.FontPath.Substring(fileNamePosition, font.FontPath.Length - fileNamePosition);

            var destinationDirectory = Path.Combine(Server.MapPath(FONT_DIRECTORY));

            var fontFilePath = string.Format(@"{0}\{1}", destinationDirectory, fileName);

            if (System.IO.File.Exists(fontFilePath))
                System.IO.File.Delete(fontFilePath);

            _fontService.Delete(fontId);

            return RedirectToAction(nameof(Index), "MediaManager");
        }

        private string SaveImage(HttpPostedFileBase imageFile, string actionName)
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

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(string.Format("Admin/Media/{0}", actionName), string.Empty);
            var relativeFilePath = string.Format("{0}{1}/{2}", siteURL, IMAGE_DIRECTORY, imageFileName);

            return relativeFilePath;
        }

        private string SaveFont(HttpPostedFileBase fontFile, string actionName)
        {
            var extension = Path.GetExtension(fontFile.FileName).ToUpper();

            if (extension != ".OTF" && extension != ".TTF")
                throw new ArgumentException("Unexpected Font Format Provided");

            var destinationDirectory = Path.Combine(Server.MapPath(FONT_DIRECTORY));

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var fontFileName = string.Format("font-{0}-{1}", DateTime.Now.ToString("ddMMyyyyHHmmss"), fontFile.FileName);
            var path = Path.Combine(Server.MapPath(FONT_DIRECTORY), fontFileName);

            fontFile.SaveAs(path);

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(string.Format("Admin/Media/{0}", actionName), string.Empty);
            var relativeFilePath = string.Format("{0}{1}/{2}", siteURL, FONT_DIRECTORY, fontFileName);

            return relativeFilePath;
        }
    }
}