using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Container;
using System;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class ContainerController : Controller
    {
        #region Dependencies

        private readonly IPageSectionService _pageSectionService;
        private readonly IPageComponentService _pageComponentService;

        public ContainerController(IPageSectionService pageSectionService, IPageComponentService pageComponentService)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int pageSectionId, string componentId)
        {
            try
            {
                _pageComponentService.Delete(pageSectionId, componentId);

                return Json(new { State = true });
            }
            catch (Exception ex)
            {
                return Json(new { State = false, Message = ex.InnerException.Message });
            }
        }
    }
}