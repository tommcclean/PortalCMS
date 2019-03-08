﻿using PortalCMS.Services.Posts;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Blog.ViewModels.Widgets;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PortalCMS.Web.Areas.BlogManager.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class WidgetsController : Controller
    {
        #region Dependencies

        readonly IPostService _postService;

        public WidgetsController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TilesWidget()
        {
            var postList = await _postService.ListByCategoryAsync(UserHelper.Id, string.Empty);

            var model = new PostsWidgetViewModel
            {
                PostList = postList.Take(6).ToList()
            };

            return View("_TilesWidget", model);
        }

        public async Task<ActionResult> RetroWidget()
        {
            var postList = await _postService.ListByCategoryAsync(UserHelper.Id, string.Empty);

            var model = new PostsWidgetViewModel
            {
                PostList = postList.Take(6).ToList()
            };

            return View("_RetroWidget", model);
        }

        public async Task<ActionResult> BoxWidget()
        {
            var postList = await _postService.ListByCategoryAsync(UserHelper.Id, string.Empty);

            var model = new PostsWidgetViewModel
            {
                PostList = postList.Take(6).ToList()
            };

            return View("_BoxWidget", model);
        }
    }
}