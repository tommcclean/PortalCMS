using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class DashboardController : Controller
    {
        #region Dependencies

        private readonly IPostService _postService;
        private readonly IPostCommentService _commentService;
        private readonly IImageService _imageService;

        public DashboardController(IPostService postService, IPostCommentService commentService, IImageService imageService)
        {
            _postService = postService;
            _commentService = commentService;
            _imageService = imageService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            var model = new DashboardViewModel()
            {
                LatestPost = _postService.GetLatest(),
                Media = _imageService.Get().ToList(),
                LatestComments = _commentService.Latest().ToList()
            };

            return View(model);
        }
    }
}