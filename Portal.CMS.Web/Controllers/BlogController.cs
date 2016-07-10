using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.ViewModels.Blog;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Controllers
{
    public class BlogController : Controller
    {
        #region Dependencies

        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;

        public BlogController(PostService postService, PostCommentService postCommentService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new BlogViewModel()
            {
                Posts = _postService.Get(null)
            };

            if (!model.Posts.Any())
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        [HttpGet]
        public ActionResult Read(int? postId)
        {
            var model = new ReadViewModel();

            if (postId.HasValue)
                model.CurrentPost = _postService.Get(postId.Value);
            else
                model.CurrentPost = _postService.GetLatest();

            if (model.CurrentPost == null || model.CurrentPost.IsPublished == false)
                return RedirectToAction("Index", "Home");

            model.RecentPosts = _postService.Get(null).Where(x => x.PostId != model.CurrentPost.PostId).ToList();
            model.SimiliarPosts = _postService.Get(model.CurrentPost.PostCategory).Where(x => x.PostId != model.CurrentPost.PostId).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(int postId, string commentBody)
        {
            _postCommentService.Add(UserHelper.UserId, postId, commentBody);

            return RedirectToAction("Read", "Blog", new { postId = postId });
        }
    }
}