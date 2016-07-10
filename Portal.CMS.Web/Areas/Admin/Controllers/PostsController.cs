using Portal.CMS.Entities.Entities.Posts;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Posts;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class PostsController : Controller
    {
        #region Dependencies

        private readonly PostService _postService;
        private readonly PostImageService _postImageService;
        private readonly ImageService _imageService;

        public PostsController(PostService postService, PostImageService postImageService, ImageService imageService)
        {
            _postService = postService;
            _postImageService = postImageService;
            _imageService = imageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new PostsViewModel()
            {
                Posts = _postService.Get(null)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePostViewModel()
            {
                ImageList = _imageService.Get()
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ImageList = _imageService.Get();

                return View("_Create", model);
            }

            var postId = _postService.Create(model.PostTitle, model.PostCategory, model.PostDescription, model.PostBody);

            if (model.PublicationState == Entities.Entities.Generic.PublicationState.Published)
                _postService.Publish(postId);

            UpdateBanner(postId, model.BannerImageId);
            UpdateGallery(postId, model.GalleryImageList);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int postId)
        {
            var post = _postService.Get(postId);

            var model = new EditPostviewModel()
            {
                PostId = post.PostId,
                PostTitle = post.PostTitle,
                PostCategory = post.PostCategory,
                PostDescription = post.PostDescription,
                PostBody = post.PostBody,
                ImageList = _imageService.Get(),
                ExistingGalleryImageList = post.PostImages.Where(x => x.PostImageType == PostImageType.Gallery).Select(x => x.ImageId).ToList()
            };

            if (post.IsPublished)
                model.PublicationState = Entities.Entities.Generic.PublicationState.Published;

            if (post.PostImages.Any(x => x.PostImageType == PostImageType.Banner))
                model.BannerImageId = post.PostImages.First(x => x.PostImageType == PostImageType.Banner).ImageId;

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostviewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ImageList = _imageService.Get();

                return View("_Edit", model);
            }

            _postService.Edit(model.PostId, model.PostTitle, model.PostCategory, model.PostDescription, model.PostBody);

            if (model.PublicationState == Entities.Entities.Generic.PublicationState.Published)
                _postService.Publish(model.PostId);
            else
                _postService.Draft(model.PostId);

            UpdateBanner(model.PostId, model.BannerImageId);
            UpdateGallery(model.PostId, model.GalleryImageList);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int postId)
        {
            _postService.Delete(postId);

            return RedirectToAction("Index", "Posts");
        }

        [HttpGet]
        public ActionResult Publish(int postId)
        {
            _postService.Publish(postId);

            return RedirectToAction("Index", "Posts");
        }

        [HttpGet]
        public ActionResult Draft(int postId)
        {
            _postService.Draft(postId);

            return RedirectToAction("Index", "Posts");
        }

        #region Private Methods

        private void UpdateBanner(int postId, int bannerImageId)
        {
            if (bannerImageId != 0)
            {
                _postImageService.Wipe(postId, PostImageType.Banner);

                _postImageService.Add(postId, bannerImageId, PostImageType.Banner);
            }
        }

        private void UpdateGallery(int postId, string galleryImageIds)
        {
            if (!string.IsNullOrWhiteSpace(galleryImageIds))
            {
                _postImageService.Wipe(postId, PostImageType.Gallery);

                var galleryImageList = galleryImageIds.Split(',');

                foreach (var image in galleryImageList)
                    _postImageService.Add(postId, Convert.ToInt32(image), PostImageType.Gallery);
            }
        }

        #endregion Private Methods
    }
}