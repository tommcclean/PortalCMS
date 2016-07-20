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
    [LoggedInFilter, AdminFilter]
    public class MediaController : Controller
    {
        #region Dependencies

        private readonly IImageService _imageService;
        private readonly IPostService _postService;
        private readonly IPostImageService _postImageService;

        private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";

        public MediaController(IPostService postService, IPostImageService postImageService, IImageService imageService)
        {
            _postService = postService;
            _postImageService = postImageService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new MediaViewModel
            {
                Images = _imageService.Get()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Upload()
        {
            var model = new UploadViewModel();

            return PartialView("_Upload", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Upload", model);

            var imageFilePath = SaveImage(model.AttachedImage, nameof(Upload));

            var imageId = _imageService.Create(imageFilePath, model.ImageCategory);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int imageId)
        {
            _imageService.Delete(imageId);

            return RedirectToAction(nameof(Index), "Media");
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
    }
}