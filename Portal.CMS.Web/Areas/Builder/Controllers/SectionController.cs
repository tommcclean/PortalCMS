using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Section;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class SectionController : Controller
    {
        #region Dependencies

        private readonly PageSectionService _pageSectionService;
        private readonly PageSectionTypeService _pageSectionTypeService;

        public SectionController(PageSectionService pageSectionService, PageSectionTypeService pageSectionTypeService)
        {
            _pageSectionService = pageSectionService;
            _pageSectionTypeService = pageSectionTypeService;
        }

        #endregion Dependencies

        [HttpGet, AdminFilter]
        public ActionResult Add(int pageId)
        {
            var model = new AddViewModel()
            {
                PageId = pageId,
                SectionTypeList = _pageSectionTypeService.Get()
            };

            return View("_Add", model);
        }

        [HttpPost, AdminFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            _pageSectionService.Add(model.PageId, model.PageSectionTypeId);

            return this.Content("Refresh");
        }

        [HttpGet, AdminFilter]
        public ActionResult Markup(int pageSectionId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var model = new MarkupViewModel()
            {
                PageSectionId = pageSectionId,
                PageSectionBody = pageSection.PageSectionBody
            };

            return View("_Markup", model);
        }

        [HttpPost, AdminFilter]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Markup(MarkupViewModel model)
        {
            _pageSectionService.Markup(model.PageSectionId, model.PageSectionBody);

            return this.Content("Refresh");
        }
    }
}