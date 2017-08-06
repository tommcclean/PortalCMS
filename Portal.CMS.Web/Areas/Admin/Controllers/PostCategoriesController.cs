using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.PostCategories;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<ActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_Add", model);
            }

            await _postCategoryService.AddAsync(model.PostCategoryName);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Edit(int postCategoryId)
        {
            var postCategory = await _postCategoryService.GetAsync(postCategoryId);

            var model = new EditViewModel
            {
                PostCategoryId = postCategoryId,
                PostCategoryName = postCategory.PostCategoryName
            };

            return View("_Edit", model);
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Modal)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_Edit", model);
            }

            await _postCategoryService.EditAsync(model.PostCategoryId, model.PostCategoryName);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> Delete(int postCategoryId)
        {
            await _postCategoryService.DeleteAsync(postCategoryId);

            return RedirectToAction("Index", "BlogManager");
        }
    }
}