using System.Web.Mvc;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.PostCategories;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class PostCategoriesController : Controller
    {
        #region Dependencies

        private readonly IPostCategoryService _postCategoryService;

        public PostCategoriesController(IPostCategoryService postCategoryService)
        {
            _postCategoryService = postCategoryService;
        }

        #endregion Dependencies

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public ActionResult Add()
        {
            var model = new AddViewModel();

            return View("_Add", model);
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Modal)]
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

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
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

        [HttpPost, AdminFilter(ActionFilterResponseType.Modal)]
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

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public ActionResult Delete(int postCategoryId)
        {
            _postCategoryService.Delete(postCategoryId);

            return RedirectToAction("Index", "BlogManager");
        }
    }
}