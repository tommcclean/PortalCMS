using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter]
    public class DashboardController : Controller
    {
        #region Dependencies

        readonly IPostService _postService;
        readonly IImageService _imageService;

        public DashboardController(IPostService postService, IImageService imageService)
        {
            _postService = postService;
            _imageService = imageService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            var model = new DashboardViewModel
            {
                LatestPost = _postService.GetLatest(),
                Media = _imageService.Get().ToList()
            };

            return View(model);
        }
    }
}