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
        private readonly IPageComponentTypeService _pageComponentTypeService; 

        public DevelopmentManagerController(IPageComponentTypeService pageComponentTypeService)
        {
            _pageComponentTypeService = pageComponentTypeService;
        }

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
            var model = new SectionTypeLibraryViewModel();

            return PartialView("_SectionTypeLibrary", model);
        }

        public ActionResult AddSectionType()
        {
            var model = new UpsertSectionTypeViewModel();

            return PartialView("_AddSectionType", model);
        }

        #endregion Section Type Management

        #region Component Type Management

        [HttpGet, ChildActionOnly]
        public ActionResult ComponentTypeLibrary()
        {
            var model = new ComponentTypeLibraryViewModel();

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

        #endregion Component Type Management
    }
}