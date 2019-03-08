﻿using PortalCMS.Services.Analytics;
using PortalCMS.Services.Authentication;
using PortalCMS.Services.Posts;
using PortalCMS.Services.Themes;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.BlogManager.ViewModels.Read;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PortalCMS.Web.Areas.BlogManager.Controllers
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
            var recentPosts = await _postService.ListByCategoryAsync(UserHelper.Id, string.Empty);

            var model = new BlogViewModel
            {
                RecentPosts = recentPosts.ToList()
            };

            if (!model.RecentPosts.Any())
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            if (id.HasValue)
                model.CurrentPost = await _postService.ReadSingleAsync(UserHelper.Id, id.Value);
            else
                model.CurrentPost = model.RecentPosts.First();

            if (model.CurrentPost == null)
                return RedirectToAction(nameof(Index), "Home", new { area = "" });

            model.Author = await _userService.GetAsync(model.CurrentPost.PostAuthorUserId);

            var similiarPosts = await _postService.ListByCategoryAsync(UserHelper.Id, model.CurrentPost.PostCategory.PostCategoryName);

            model.SimiliarPosts = similiarPosts.ToList();

            model.RecentPosts.Remove(model.CurrentPost);
            model.SimiliarPosts.Remove(model.CurrentPost);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Comment(int postId, string commentBody)
        {
            await _postCommentService.AddAsync(UserHelper.Id, postId, commentBody);

            return RedirectToAction(nameof(Index), "Read", new { postId = postId });
        }

        public async Task<ActionResult> Analytic(int postId, string referrer)
        {
            if (UserHelper.IsLoggedIn)
                await _analyticsService.LogPostViewAsync(postId, referrer, Request.UserHostAddress, Request.Browser.Browser, UserHelper.Id);
            else
                await _analyticsService.LogPostViewAsync(postId, referrer, Request.UserHostAddress, Request.Browser.Browser, null);

            return Json(new { State = true });
        }
    }
}