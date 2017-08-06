using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter(ActionFilterResponseType.Page)]
    public class DashboardController : Controller
    {
        #region Dependencies

        private readonly IPostService _postService;
        private readonly IImageService _imageService;

        public DashboardController(IPostService postService, IImageService imageService)
        {
            _postService = postService;
            _imageService = imageService;
        }

        #endregion Dependencies

        public async Task<ActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                LatestPost = await _postService.GetLatestAsync(),
                Media = await _imageService.GetAsync()
            };

            return View(model);
        }
    }
}