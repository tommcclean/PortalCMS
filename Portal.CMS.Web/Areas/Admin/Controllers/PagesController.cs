using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Pages;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class PagesController : Controller
    {
        #region Dependencies

        private readonly PageService _pageService;

        public PagesController(PageService pageService)
        {
            _pageService = pageService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new PagesViewModel()
            {
                PageList = _pageService.Get()
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

            _pageService.Add(model.PageName, model.PageArea, model.PageController, model.PageAction);

            System.Web.HttpRuntime.UnloadAppDomain();

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int pageId)
        {
            var page = _pageService.Get(pageId);

            var model = new EditViewModel()
            {
                PageId = page.PageId,
                PageName = page.PageName,
                PageArea = page.PageArea,
                PageController = page.PageController,
                PageAction = page.PageAction
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            var page = _pageService.Get(model.PageId);

            var restartRequired = false;

            if (!string.IsNullOrWhiteSpace(page.PageArea).Equals(string.IsNullOrWhiteSpace(model.PageArea)) || !page.PageController.Equals(model.PageController) || !page.PageAction.Equals(model.PageAction))
                restartRequired = true;

            if (!ModelState.IsValid)
                return View("_Create", model);

            _pageService.Edit(model.PageId, model.PageName, model.PageArea, model.PageController, model.PageAction);

            // RESET: Routing by Starting the Website.
            if (restartRequired)
                System.Web.HttpRuntime.UnloadAppDomain();

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int pageId)
        {
            _pageService.Delete(pageId);

            return RedirectToAction("Index");
        }
    }
}