using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Services.Shared;
using Portal.CMS.Web.Areas.Builder.ViewModels.Component;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    public class ComponentController : Controller
    {
        #region Dependencies

        private readonly PageSectionService _pageSectionService;
        private readonly PageComponentTypeService _pageComponentTypeService;
        private readonly PageComponentService _pageComponentService;

        public ComponentController(PageSectionService pageSectionService, PageComponentTypeService pageComponentTypeService, PageComponentService pageComponentService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentTypeService = pageComponentTypeService;
            _pageComponentService = pageComponentService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Add(int pageSectionId, string elementId)
        {
            var model = new AddViewModel()
            {
                PageSectionId = pageSectionId,
                PageComponentTypeList = _pageComponentTypeService.Get(),
                ContainerElementId = elementId
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            _pageComponentTypeService.Add(model.PageSectionId, model.ContainerElementId, model.PageComponentTypeId);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int pageSectionId, string componentId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            _pageComponentService.Delete(pageSectionId, componentId);

            return RedirectToAction("Index", "Build", new { pageId = pageSection.PageId });
        }

        #region Anchor Component

        [HttpGet]
        public ActionResult Anchor(int pageSectionId, string elementId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            var document = new Document(pageSection.PageSectionBody);

            var model = new AnchorViewModel()
            {
                SectionId = pageSectionId,
                ElementId = elementId,
                ElementText = document.GetContent(elementId),
                ElementTarget = document.GetAttribute(elementId, "href"),
                ElementColour = ""
            };

            return View("_Anchor", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anchor(AnchorViewModel model)
        {
            _pageComponentService.Anchor(model.SectionId, model.ElementId, model.ElementText, model.ElementTarget, model.ElementColour);

            return this.Content("Refresh");
        }

        #endregion
    }
}