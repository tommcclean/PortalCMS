using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Builder.ViewModels.Section;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class SectionController : Controller
    {
        #region Dependencies

        private readonly PageSectionService _pageSectionService;

        public SectionController(PageSectionService pageSectionService)
        {
            _pageSectionService = pageSectionService;
        }

        #endregion Dependencies

        [HttpGet]
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

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Markup(MarkupViewModel model)
        {
            _pageSectionService.Markup(model.PageSectionId, model.PageSectionBody);

            return this.Content("Refresh");
        }
    }
}