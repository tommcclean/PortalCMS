using Portal.CMS.Services.Copy;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.CopyManager;
using System;
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

        [HttpGet, AdminFilter]
        public ActionResult Index()
        {
            var model = new CopyViewModel()
            {
                CopyList = _copyService.Get()
            };

            return View(model);
        }

        [HttpGet, AdminFilter]
        public ActionResult Create()
        {
            var model = new CreateViewModel()
            {
            };

            return View("_Create", model);
        }

        [HttpPost, AdminFilter]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _copyService.Create(model.CopyName, model.CopyBody);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Edit(int copyId)
        {
            var copy = _copyService.Get(copyId);

            var model = new EditViewModel()
            {
                CopyId = copy.CopyId,
                CopyName = copy.CopyName,
                CopyBody = copy.CopyBody
            };

            return View("_Edit", model);
        }

        [HttpPost, AdminFilter]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _copyService.Edit(model.CopyId, model.CopyName, model.CopyBody);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Delete(int copyId)
        {
            _copyService.Delete(copyId);

            return RedirectToAction("Index");
        }

        [HttpPost, AdminFilter]
        [ValidateInput(false)]
        public ActionResult Inline(int copyId, string copyName, string copyBody)
        {
            _copyService.Edit(copyId, copyName, copyBody);

            return Content("Refresh");
        }

        [ChildActionOnly]
        public ActionResult Get(string copyName)
        {
            if (string.IsNullOrWhiteSpace(copyName))
                throw new ArgumentException("Copy name must be specified");

            var copy = _copyService.Get(copyName);

            if (copy == null)
            {
                var copyId = _copyService.Create(copyName, "This is example copy");

                copy = _copyService.Get(copyId);
            }

            return View("_Copy", copy);
        }
    }
}