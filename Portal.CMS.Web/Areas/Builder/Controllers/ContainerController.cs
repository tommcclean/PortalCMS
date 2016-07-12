using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Container;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class ContainerController : Controller
    {
        #region Dependencies

        private readonly PageSectionService _pageSectionService;
        private readonly PageComponentService _pageComponentService;

        public ContainerController(PageSectionService pageSectionService, PageComponentService pageComponentService)
        {
            _pageSectionService = pageSectionService;
            _pageComponentService = pageComponentService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Edit(int pageSectionId, string elementId)
        {
            var model = new EditViewModel()
            {
                PageSectionId = pageSectionId,
                ContainerElementId = elementId
            };

            return View("_Edit", model);
        }

        public ActionResult Delete(int pageSectionId, string elementId)
        {
            var pageSection = _pageSectionService.Get(pageSectionId);

            _pageComponentService.Delete(pageSectionId, elementId);

            return RedirectToAction("Index", "Build", new { pageId = pageSection.PageId });
        }
    }
}