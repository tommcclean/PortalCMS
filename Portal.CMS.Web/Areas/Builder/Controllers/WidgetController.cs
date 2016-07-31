using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Areas.Builder.ViewModels.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class WidgetController : Controller
    {
        #region Dependencies

        private readonly IPostService _postService;

        public WidgetController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion Dependencies

        // GET: Builder/Widget
        public ActionResult RecentPostList()
        {
            var model = new PostListViewModel
            {
                PostList = _postService.Get(string.Empty, true).Take(6).ToList()
            };

            return View("_RecentPostList", model);
        }
    }
}