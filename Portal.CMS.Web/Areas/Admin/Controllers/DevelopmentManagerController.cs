using System.Web.Mvc;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Shared;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.DevelopmentManager;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class DevelopmentManagerController : Controller
    {
        #region Dependencies

        private readonly IPageSectionTypeService _pageSectionTypeService;
        private readonly IPageComponentTypeService _pageComponentTypeService;

        public DevelopmentManagerController(IPageComponentTypeService pageComponentTypeService, IPageSectionTypeService pageSectionTypeService)
        {
            _pageComponentTypeService = pageComponentTypeService;
            _pageSectionTypeService = pageSectionTypeService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region Widget Management

        [HttpGet, ChildActionOnly]
        public ActionResult CustomWidgets()
        {
            var model = new CustomWidgetsViewModel();

            return PartialView("_CustomWidgets", model);
        }

        public ActionResult AddCustomWidget()
        {
            var model = new UpsertCustomWidgetViewModel();

            return PartialView("_AddCustomWidget", model);
        }

        #endregion Widget Management

        #region Section Type Management

        [HttpGet, ChildActionOnly]
        public ActionResult SectionTypeLibrary()
        {
            var model = new SectionTypeLibraryViewModel
            {
                PageSectionTypes = _pageSectionTypeService.Get()
            };

            return PartialView("_SectionTypeLibrary", model);
        }

        public ActionResult AddSectionType()
        {
            var model = new UpsertSectionTypeViewModel();

            return PartialView("_AddSectionType", model);
        }

        [HttpGet]
        public ActionResult ResetSectionLibrary()
        {
            _pageSectionTypeService.Reset();

            return RedirectToAction(nameof(Index));
        }

        #endregion Section Type Management

        #region Component Type Management

        [HttpGet, ChildActionOnly]
        public ActionResult ComponentTypeLibrary()
        {
            var model = new ComponentTypeLibraryViewModel
            {
                PageComponentTypes = _pageComponentTypeService.Get()
            };

            return PartialView("_ComponentTypeLibrary", model);
        }

        [HttpGet]
        public ActionResult AddComponentType()
        {
            var model = new UpsertComponentTypeViewModel();

            return PartialView("_AddComponentType", model);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult AddComponentType(UpsertComponentTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_AddComponentType", model);

            var document = new Document(model.ComponentTypeBody);

            document.LabelElements();

            var outputMarkup = document.OuterHtml;

            _pageComponentTypeService.Add(model.ComponentTypeName, model.ComponentTypeCategory, outputMarkup);

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult ResetComponentLibrary()
        {
            _pageComponentTypeService.Reset();

            return RedirectToAction(nameof(Index));
        }

        #endregion Component Type Management
    }
}