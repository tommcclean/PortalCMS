using Portal.CMS.Entities.Entities.Generic;
using Portal.CMS.Entities.Entities.Posts;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.BlogManager;
using Portal.CMS.Web.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter]
    public class BlogManagerController : Controller
    {
        #region Manifest Constants

        const string BANNER_IMAGE_ID = "BannerImageId;";
        const string BANNER = "Banner";
        const string GALLERY_IMAGE_LIST = "GalleryImageList";
        const string GALLERY = "Gallery";

        #endregion Manifest Constants

        #region Dependencies

        private readonly IPostService _postService;
        private readonly IPostImageService _postImageService;
        private readonly IImageService _imageService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public BlogManagerController(IPostService postService, IPostImageService postImageService, IImageService imageService, IPostCategoryService postCategoryService, IUserService userService, IRoleService roleService)
        {
            _postService = postService;
            _postImageService = postImageService;
            _imageService = imageService;
            _postCategoryService = postCategoryService;
            _userService = userService;
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet, AdminFilter]
        public ActionResult Index()
        {
            var model = new PostsViewModel
            {
                Posts = _postService.Get(string.Empty, false),
                PostCategories = _postCategoryService.Get()
            };

            return View(model);
        }

        [HttpGet, EditorFilter]
        public ActionResult Create()
        {
            var postCategories = _postCategoryService.Get();

            var model = new CreatePostViewModel
            {
                PostCategoryId = postCategories.First().PostCategoryId,
                PostAuthorUserId = UserHelper.UserId.Value,
                PostCategoryList = postCategories,
                UserList = _userService.Get(new List<string> { nameof(Admin), "Editor" }),
                PublicationState = PublicationState.Published,
                BannerImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                },
                GalleryImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                },
                RoleList = _roleService.Get()
            };

            return View("_Create", model);
        }

        [HttpPost, EditorFilter]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BannerImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                };
                model.GalleryImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                };
                model.PostCategoryList = _postCategoryService.Get();
                model.UserList = _userService.Get(new List<string> { nameof(Admin), "Editor" });
                model.RoleList = _roleService.Get();

                return View("_Create", model);
            }

            var postId = _postService.Create(model.PostTitle, model.PostCategoryId, model.PostAuthorUserId, model.PostDescription, model.PostBody);

            if (model.PublicationState == PublicationState.Published)
                _postService.Publish(postId);

            UpdateBanner(postId, model.BannerImageId);
            UpdateGallery(postId, model.GalleryImageList);

            _postService.Roles(postId, model.SelectedRoleList);

            return this.Content("Blog");
        }

        [HttpGet, EditorFilter]
        public ActionResult Edit(int postId)
        {
            var post = _postService.Get(postId);

            var model = new EditPostviewModel
            {
                PostId = post.PostId,
                PostTitle = post.PostTitle,
                PostDescription = post.PostDescription,
                PostBody = post.PostBody,
                PostCategoryId = post.PostCategoryId,
                PostAuthorUserId = post.PostAuthorUserId,
                ExistingGalleryImageList = post.PostImages.Where(x => x.PostImageType == PostImageType.Gallery).Select(x => x.ImageId).ToList(),
                PostCategoryList = _postCategoryService.Get(),
                UserList = _userService.Get(new List<string> { nameof(Admin), "Editor" }),
                BannerImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                },
                GalleryImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                },
                RoleList = _roleService.Get(),
                SelectedRoleList = post.PostRoles.Select(x => x.Role.RoleName).ToList()
            };

            if (post.IsPublished)
                model.PublicationState = PublicationState.Published;

            if (post.PostImages.Any(x => x.PostImageType == PostImageType.Banner))
                model.BannerImageId = post.PostImages.First(x => x.PostImageType == PostImageType.Banner).ImageId;

            return View("_Edit", model);
        }

        [HttpPost, EditorFilter]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostviewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BannerImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                };
                model.GalleryImages = new PaginationViewModel
                {
                    ImageList = _imageService.Get().Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                };
                model.PostCategoryList = _postCategoryService.Get();
                model.UserList = _userService.Get(new List<string> { nameof(Admin), "Editor" });
                model.RoleList = _roleService.Get();

                return View("_Edit", model);
            }

            _postService.Edit(model.PostId, model.PostTitle, model.PostCategoryId, model.PostAuthorUserId, model.PostDescription, model.PostBody);

            if (model.PublicationState == PublicationState.Published)
                _postService.Publish(model.PostId);
            else
                _postService.Draft(model.PostId);

            UpdateBanner(model.PostId, model.BannerImageId);
            UpdateGallery(model.PostId, model.GalleryImageList);

            _postService.Roles(model.PostId, model.SelectedRoleList);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Delete(int postId)
        {
            _postService.Delete(postId);

            return RedirectToAction(nameof(Index), "BlogManager");
        }

        [HttpGet, AdminFilter]
        public ActionResult Publish(int postId)
        {
            _postService.Publish(postId);

            return RedirectToAction(nameof(Index), "BlogManager");
        }

        [HttpGet, AdminFilter]
        public ActionResult Draft(int postId)
        {
            _postService.Draft(postId);

            return RedirectToAction(nameof(Index), "BlogManager");
        }

        [HttpPost, EditorFilter]
        [ValidateInput(false)]
        public ActionResult Inline(int postId, string markup)
        {
            _postService.Edit(postId, markup);

            return Content("Refresh");
        }

        [HttpPost, EditorFilter]
        [ValidateInput(false)]
        public ActionResult Description(int postId, string markup)
        {
            _postService.Description(postId, markup);

            return Content("Refresh");
        }

        [HttpPost, EditorFilter]
        [ValidateInput(false)]
        public ActionResult Headline(int postId, string markup)
        {
            _postService.Headline(postId, markup);

            return Content("Refresh");
        }

        #region Private Methods

        void UpdateBanner(int postId, int bannerImageId)
        {
            if (bannerImageId != 0)
            {
                _postImageService.Remove(postId, PostImageType.Banner);

                _postImageService.Add(postId, bannerImageId, PostImageType.Banner);
            }
        }

        void UpdateGallery(int postId, string galleryImageIds)
        {
            if (!string.IsNullOrWhiteSpace(galleryImageIds))
            {
                _postImageService.Remove(postId, PostImageType.Gallery);

                var galleryImageList = galleryImageIds.Split(',');

                foreach (var image in galleryImageList)
                    _postImageService.Add(postId, Convert.ToInt32(image), PostImageType.Gallery);
            }
        }

        #endregion Private Methods
    }
}