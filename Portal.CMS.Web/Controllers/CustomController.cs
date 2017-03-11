using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.ViewModels.Custom;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Controllers
{
    public class CustomController : Controller
    {
        #region Dependencies

        private readonly IPostService _postService;

        public CustomController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion Dependencies

        [ChildActionOnly]
        public ActionResult Simple(int pageId)
        {
            return View("_SimplePartial");
        }

        [ChildActionOnly]
        public ActionResult Advanced(int pageId)
        {
            var model = new AdvancedViewModel
            {
                PostList = _postService.Read(UserHelper.UserId, string.Empty).ToList().Take(3).ToList()
            };

            return View("_AdvancedPartial", model);
        }
    }
}