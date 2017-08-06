using Portal.CMS.Services.Copy;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.CopyManager;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class CopyManagerController : Controller
    {
        #region Dependencies

        private readonly ICopyService _copyService;

        public CopyManagerController(ICopyService copyService)
        {
            _copyService = copyService;
        }

        #endregion Dependencies

        [HttpGet, AdminFilter(ActionFilterResponseType.Page)]
        public async Task<ActionResult> Index()
        {
            var model = new CopyViewModel
            {
                CopyList = await _copyService.GetAsync()
            };

            return View(model);
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public ActionResult Create()
        {
            var model = new CreateViewModel();

            return View("_Create", model);
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Modal)]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            await _copyService.CreateAsync(model.CopyName, model.CopyBody);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Edit(int copyId)
        {
            var copy = await _copyService.GetAsync(copyId);

            var model = new EditViewModel
            {
                CopyId = copy.CopyId,
                CopyName = copy.CopyName,
                CopyBody = copy.CopyBody
            };

            return View("_Edit", model);
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Modal)]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            await _copyService.EditAsync(model.CopyId, model.CopyName, model.CopyBody);

            return Content("Refresh");
        }

        [HttpGet, AdminFilter(ActionFilterResponseType.Modal)]
        public async Task<ActionResult> Delete(int copyId)
        {
            await _copyService.DeleteAsync(copyId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, AdminFilter(ActionFilterResponseType.Page)]
        [ValidateInput(false)]
        public async Task<ActionResult> Inline(int copyId, string copyName, string copyBody)
        {
            await _copyService.EditAsync(copyId, copyName, copyBody);

            return Content("Refresh");
        }

        public async Task<ActionResult> Get(string copyName)
        {
            if (string.IsNullOrWhiteSpace(copyName))
                throw new ArgumentException("Copy name must be specified");

            var copy = await _copyService.GetAsync(copyName);

            if (copy == null)
            {
                var copyId = await _copyService.CreateAsync(copyName, "This is example copy");

                copy = await _copyService.GetAsync(copyId);
            }

            return View("_Copy", copy);
        }

        [ChildActionOnly]
        public ActionResult Render(string copyName)
        {
            if (string.IsNullOrWhiteSpace(copyName))
                throw new ArgumentException("Copy name must be specified");

            var copy = AsyncHelpers.RunSync(() => _copyService.GetAsync(copyName));

            if (copy == null)
            {
                var copyId = AsyncHelpers.RunSync(() => _copyService.CreateAsync(copyName, "This is example copy"));

                copy = AsyncHelpers.RunSync(() => _copyService.GetAsync(copyId));
            }

            return View("_Copy", copy);
        }
    }
}