using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Builder.ViewModels.Widget;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class WidgetController : Controller
    {
        #region Dependencies

        readonly IPostService _postService;

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
                PostList = _postService.Read(UserHelper.UserId, string.Empty).Take(6).ToList()
            };

            return View("_RecentPostList", model);
        }
    }
}