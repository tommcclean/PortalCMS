using Portal.CMS.Services.Analytics;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Posts;
using Portal.CMS.Services.Themes;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.BlogManager.ViewModels.Read;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.BlogManager.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ReadController : Controller
    {
        #region Dependencies

        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;
        private readonly IAnalyticsService _analyticsService;
        private readonly IUserService _userService;
        private readonly IThemeService _themeService;

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
        public async Task<ActionResult> Index(int? id)
        {
            var recentPosts = await _postService.ReadAsync(UserHelper.UserId, string.Empty);

            var model = new BlogViewModel
            {
                RecentPosts = recentPosts.ToList()
            };

            if (!model.RecentPosts.Any())
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            if (id.HasValue)
                model.CurrentPost = await _postService.ReadAsync(UserHelper.UserId, id.Value);
            else
                model.CurrentPost = model.RecentPosts.First();

            if (model.CurrentPost == null)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            model.Author = await _userService.GetUserAsync(model.CurrentPost.PostAuthorUserId);

            var similiarPosts = await _postService.ReadAsync(UserHelper.UserId, model.CurrentPost.PostCategory.PostCategoryName);

            model.SimiliarPosts = similiarPosts.ToList();

            model.RecentPosts.Remove(model.CurrentPost);
            model.SimiliarPosts.Remove(model.CurrentPost);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Comment(int postId, string commentBody)
        {
            await _postCommentService.AddAsync(UserHelper.UserId.Value, postId, commentBody);

            return RedirectToAction(nameof(Index), "Read", new { postId = postId });
        }

        public async Task<ActionResult> Analytic(int postId, string referrer)
        {
            if (UserHelper.IsLoggedIn)
                await _analyticsService.LogPostViewAsync(postId, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.UserId);
            else
                await _analyticsService.LogPostViewAsync(postId, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }
    }
}