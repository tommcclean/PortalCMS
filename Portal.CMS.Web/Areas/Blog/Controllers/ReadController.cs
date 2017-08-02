using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Posts;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.BlogManager.ViewModels.Read;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.BlogManager.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ReadController : Controller
    {
        #region Dependencies

        readonly IPostService _postService;
        readonly IPostCommentService _postCommentService;
        readonly IAnalyticsService _analyticsService;
        readonly IUserService _userService;
        readonly IThemeService _themeService;

        public ReadController(IPostService postService, IPostCommentService postCommentService, IAnalyticsService analyticsService, IUserService userService, IThemeService themeService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _analyticsService = analyticsService;
            _userService = userService;
            _themeService = themeService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index(int? id)
        {
            var model = new BlogViewModel
            {
                RecentPosts = _postService.Read(UserHelper.UserId, string.Empty).ToList()
            };

            if (!model.RecentPosts.Any())
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            if (id.HasValue)
                model.CurrentPost = _postService.Read(UserHelper.UserId, id.Value);
            else
                model.CurrentPost = model.RecentPosts.First();

            if (model.CurrentPost == null)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            model.Author = _userService.GetUser(model.CurrentPost.PostAuthorUserId);

            model.SimiliarPosts = _postService.Read(UserHelper.UserId, model.CurrentPost.PostCategory.PostCategoryName).ToList();

            model.RecentPosts.Remove(model.CurrentPost);
            model.SimiliarPosts.Remove(model.CurrentPost);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Comment(int postId, string commentBody)
        {
            _postCommentService.Add(UserHelper.UserId.Value, postId, commentBody);

            return RedirectToAction(nameof(Index), "Read", new { postId = postId });
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