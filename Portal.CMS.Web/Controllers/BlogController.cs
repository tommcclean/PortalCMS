using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Authentication;
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
        private readonly IAnalyticsService _analyticsService;
        private readonly IUserService _userService;

        public BlogController(IPostService postService, IPostCommentService postCommentService, IAnalyticsService analyticsService, IUserService userService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _analyticsService = analyticsService;
            _userService = userService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index(int? id)
        {
            var model = new BlogViewModel
            {
                RecentPosts = _postService.Read(UserHelper.UserId, string.Empty).ToList(),
            };

            if (!model.RecentPosts.Any())
                return RedirectToAction("Index", "Home");

            if (id.HasValue)
                model.CurrentPost = _postService.Read(UserHelper.UserId, id.Value);
            else
                model.CurrentPost = model.RecentPosts.First();

            if (model.CurrentPost == null)
                return RedirectToAction("Index", "Home");

            model.Author = _userService.GetUser(model.CurrentPost.PostAuthorUserId);

            model.SimiliarPosts = _postService.Read(UserHelper.UserId, model.CurrentPost.PostCategory.PostCategoryName).ToList();

            model.RecentPosts.Remove(model.CurrentPost);
            model.SimiliarPosts.Remove(model.CurrentPost);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(int postId, string commentBody)
        {
            _postCommentService.Add(UserHelper.UserId.Value, postId, commentBody);

            return RedirectToAction(nameof(Index), "Blog", new { postId = postId });
        }

        public ActionResult Analytic(int postId, string referrer)
        {
            if (UserHelper.IsLoggedIn)
                _analyticsService.LogPostView(postId, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.UserId);
            else
                _analyticsService.LogPostView(postId, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }
    }
}