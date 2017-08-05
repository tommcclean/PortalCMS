using Portal.CMS.Entities.Enumerators;
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
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class BlogManagerController : Controller
    {
        #region Manifest Constants

        private const string BANNER_IMAGE_ID = "BannerImageId";
        private const string BANNER = "Banner";
        private const string GALLERY_IMAGE_LIST = "GalleryImageList";
        private const string GALLERY = "Gallery";

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

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public ActionResult Index()
        {
            var model = new PostsViewModel
            {
                Posts = _postService.Get(string.Empty, false),
                PostCategories = _postCategoryService.Get()
            };

            return View(model);
        }

        [HttpGet, EditorFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Create()
        {
            var postCategories = _postCategoryService.Get();

            var imageList = await _imageService.GetAsync();

            var model = new CreatePostViewModel
            {
                PostCategoryId = postCategories.First().PostCategoryId,
                PostAuthorUserId = UserHelper.UserId.Value,
                PostCategoryList = postCategories,
                UserList = await _userService.GetAsync(new List<string> { nameof(Admin), "Editor" }),
                PublicationState = PublicationState.Published,
                BannerImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                },
                GalleryImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                },
                RoleList = await _roleService.GetAsync()
            };

            return View("_Create", model);
        }

        [HttpPost, EditorFilter(ActionFilterResponseType.Modal)]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePostViewModel model)
        {
            var imageList = await _imageService.GetAsync();

            if (!ModelState.IsValid)
            {
                model.BannerImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                };
                model.GalleryImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                };
                model.PostCategoryList = _postCategoryService.Get();
                model.UserList = await _userService.GetAsync(new List<string> { nameof(Admin), "Editor" });
                model.RoleList = await _roleService.GetAsync();

                return View("_Create", model);
            }

            var postId = _postService.Create(model.PostTitle, model.PostCategoryId, model.PostAuthorUserId, model.PostDescription, model.PostBody);

            if (model.PublicationState == PublicationState.Published)
                _postService.Publish(postId);

            UpdateBanner(postId, model.BannerImageId);
            UpdateGallery(postId, model.GalleryImageList);

            _postService.Roles(postId, model.SelectedRoleList);

            return Content("Blog");
        }

        [HttpGet, EditorFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Edit(int postId)
        {
            var post = _postService.Get(postId);

            var imageList = await _imageService.GetAsync();

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
                UserList = await _userService.GetAsync(new List<string> { nameof(Admin), "Editor" }),
                BannerImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                },
                GalleryImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                },
                RoleList = await _roleService.GetAsync(),
                SelectedRoleList = post.PostRoles.Select(x => x.Role.RoleName).ToList()
            };

            if (post.IsPublished)
                model.PublicationState = PublicationState.Published;

            if (post.PostImages.Any(x => x.PostImageType == PostImageType.Banner))
                model.BannerImageId = post.PostImages.First(x => x.PostImageType == PostImageType.Banner).ImageId;

            return View("_Edit", model);
        }

        [HttpPost, EditorFilter(ActionFilterResponseType.Modal)]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditPostviewModel model)
        {
            var imageList = await _imageService.GetAsync();

            if (!ModelState.IsValid)
            {
                model.BannerImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = BANNER_IMAGE_ID,
                    PaginationType = BANNER
                };
                model.GalleryImages = new PaginationViewModel
                {
                    ImageList = imageList.Where(x => x.ImageCategory == ImageCategory.General || x.ImageCategory == ImageCategory.Screenshot || x.ImageCategory == ImageCategory.Texture),
                    TargetInputField = GALLERY_IMAGE_LIST,
                    PaginationType = GALLERY
                };
                model.PostCategoryList = _postCategoryService.Get();
                model.UserList = await _userService.GetAsync(new List<string> { nameof(Admin), "Editor" });
                model.RoleList = await _roleService.GetAsync();

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

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public ActionResult Delete(int postId)
        {
            _postService.Delete(postId);

            return RedirectToAction(nameof(Index), "BlogManager");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public ActionResult Publish(int postId)
        {
            _postService.Publish(postId);

            return RedirectToAction(nameof(Index), "BlogManager");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public ActionResult Draft(int postId)
        {
            _postService.Draft(postId);

            return RedirectToAction(nameof(Index), "BlogManager");
        }

        [HttpPost, EditorFilter(ActionFilterResponseType.Page)]
        [ValidateInput(false)]
        public ActionResult Inline(int postId, string markup)
        {
            _postService.Edit(postId, markup);

            return Content("Refresh");
        }

        [HttpPost, EditorFilter(ActionFilterResponseType.Page)]
        [ValidateInput(false)]
        public ActionResult Description(int postId, string markup)
        {
            _postService.Description(postId, markup);

            return Content("Refresh");
        }

        [HttpPost, EditorFilter(ActionFilterResponseType.Page)]
        [ValidateInput(false)]
        public ActionResult Headline(int postId, string markup)
        {
            _postService.Headline(postId, markup);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> AppDrawer()
        {
            var postList = await _postService.ReadAsync(UserHelper.UserId, string.Empty);

            var model = new AppDrawerViewModel
            {
                PostList = postList.ToList()
            };

            return PartialView("_AppDrawer", model);
        }

        #region Private Methods

        private void UpdateBanner(int postId, int bannerImageId)
        {
            if (bannerImageId != 0)
            {
                _postImageService.Remove(postId, PostImageType.Banner);

                _postImageService.Add(postId, bannerImageId, PostImageType.Banner);
            }
        }

        private void UpdateGallery(int postId, string galleryImageIds)
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