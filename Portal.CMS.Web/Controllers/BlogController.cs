using Portal.CMS.Services.Analytics;
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
        private readonly AnalyticsService _analyticsService;

        public BlogController(PostService postService, PostCommentService postCommentService, AnalyticsService analyticsService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _analyticsService = analyticsService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new BlogViewModel()
            {
                Posts = _postService.Get(null, true)
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

            model.RecentPosts = _postService.Get(string.Empty, true).Where(x => x.PostId != model.CurrentPost.PostId).Take(10).ToList();
            model.SimiliarPosts = _postService.Get(model.CurrentPost.PostCategory.PostCategoryName, true).Where(x => x.PostId != model.CurrentPost.PostId).Take(10).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(int postId, string commentBody)
        {
            _postCommentService.Add(UserHelper.UserId, postId, commentBody);

            return RedirectToAction("Read", "Blog", new { postId = postId });
        }

        public ActionResult Analytic(int postId, string referrer)
        {
            if (UserHelper.IsLoggedIn)
                _analyticsService.AnalysePostView(postId, referrer, UserHelper.UserId);
            else
                _analyticsService.AnalysePostView(postId, referrer, null);

            return Json(new { State = true });
        }
    }
}