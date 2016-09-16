using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Media;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter]
    public class MediaController : Controller
    {
        #region Dependencies

        readonly IImageService _imageService;
        readonly IFontService _fontService;
        readonly IPostService _postService;
        readonly IPostImageService _postImageService;

        const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";
        const string FONT_DIRECTORY = "/Areas/Admin/Content/Fonts/Uploads";

        public MediaController(IPostService postService, IPostImageService postImageService, IImageService imageService, IFontService fontService)
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

            return RedirectToAction(nameof(Index), "Media");
        }


        string SaveImage(HttpPostedFileBase imageFile, string actionName)
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

        string SaveFont(HttpPostedFileBase fontFile, string actionName)
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