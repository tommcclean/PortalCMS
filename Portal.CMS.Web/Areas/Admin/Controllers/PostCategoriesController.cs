using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.PostCategories;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class PostCategoriesController : Controller
    {
        #region Dependencies

        readonly IPostCategoryService _postCategoryService;

        public PostCategoriesController(IPostCategoryService postCategoryService)
        {
            _postCategoryService = postCategoryService;
        }

        #endregion Dependencies

        [HttpGet, AdminModalFilter]
        public ActionResult Add()
        {
            var model = new AddViewModel();

            return View("_Add", model);
        }

        [HttpPost, AdminModalFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_Add", model);
            }

            _postCategoryService.Add(model.PostCategoryName);

            return Content("Refresh");
        }

        [HttpGet, AdminModalFilter]
        public ActionResult Edit(int postCategoryId)
        {
            var postCategory = _postCategoryService.Get(postCategoryId);

            var model = new EditViewModel
            {
                PostCategoryId = postCategoryId,
                PostCategoryName = postCategory.PostCategoryName
            };

            return View("_Edit", model);
        }

        [HttpPost, AdminModalFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_Edit", model);
            }

            _postCategoryService.Edit(model.PostCategoryId, model.PostCategoryName);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Delete(int postCategoryId)
        {
            _postCategoryService.Delete(postCategoryId);

            return RedirectToAction("Index", "BlogManager");
        }
    }
}