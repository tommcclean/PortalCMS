using Portal.CMS.Services.Copy;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Copy;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class CopyController : Controller
    {
        #region Dependencies

        private readonly ICopyService _copyService;

        public CopyController(ICopyService copyService)
        {
            _copyService = copyService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new CopyViewModel()
            {
                CopyList = _copyService.Get()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel()
            {
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _copyService.Create(model.CopyName, model.CopyBody);

            return this.Content("Refresh");
        }

        [HttpGet]
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

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _copyService.Edit(model.CopyId, model.CopyName, model.CopyBody);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int copyId)
        {
            _copyService.Delete(copyId);

            return RedirectToAction("Index");
        }
    }
}