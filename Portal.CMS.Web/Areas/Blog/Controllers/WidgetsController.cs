using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Blog.ViewModels.Widgets;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.BlogManager.Controllers
{
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

        public ActionResult TilesWidget()
        {
            var model = new PostsWidgetViewModel
            {
                PostList = _postService.Read(UserHelper.UserId, string.Empty).Take(6).ToList()
            };

            return View("_TilesWidget", model);
        }

        public ActionResult RetroWidget()
        {
            var model = new PostsWidgetViewModel
            {
                PostList = _postService.Read(UserHelper.UserId, string.Empty).Take(6).ToList()
            };

            return View("_RetroWidget", model);
        }
    }
}