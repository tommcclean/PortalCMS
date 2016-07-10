using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Comments;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class CommentsController : Controller
    {
        #region Dependencies

        private readonly IPostCommentService _postCommentService;

        public CommentsController(PostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new CommentsViewModel()
            {
                PostComments = _postCommentService.Get().ToList()
            };

            return View(model);
        }
    }
}