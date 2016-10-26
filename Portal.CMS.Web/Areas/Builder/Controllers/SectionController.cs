using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Section;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Builder.Controllers
{
    [AdminFilter]
    public class SectionController : Controller
    {
        #region Dependencies

        private readonly IPageSectionService _pageSectionService;
        private readonly IPageSectionTypeService _pageSectionTypeService;
        private readonly IImageService _imageService;
        private readonly IRoleService _roleService;

        public SectionController(IPageSectionService pageSectionService, IPageSectionTypeService pageSectionTypeService, IImageService imageService, IRoleService roleService)
        {
            _pageSectionService = pageSectionService;
            _pageSectionTypeService = pageSectionTypeService;
            _imageService = imageService;
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Edit(int sectionId)
        {
            var pageSection = _pageSectionService.Get(sectionId);

            var model = new EditViewModel()
            {
                PageId = pageSection.PageId,
                SectionId = sectionId,
                ImageList = _imageService.Get(),
                PageSectionHeight = _pageSectionService.DetermineSectionHeight(sectionId),
                PageSectionBackgroundStyle = _pageSectionService.DetermineBackgroundStyle(sectionId),
                BackgroundType = _pageSectionService.DetermineBackgroundType(sectionId),
                BackgroundColour = "#ffffff",
                RoleList = _roleService.Get(),
                SelectedRoleList = pageSection.PageSectionRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if ("colour".Equals(model.BackgroundType, StringComparison.OrdinalIgnoreCase))
            {
                _pageSectionService.SetBackgroundType(model.SectionId, false);

                if (!string.IsNullOrWhiteSpace(model.BackgroundColour))
                    _pageSectionService.Background(model.SectionId, model.BackgroundColour);
            }
            else
            {
                _pageSectionService.SetBackgroundType(model.SectionId, true);

                if (model.BackgroundImageId > 0)
                    _pageSectionService.Background(model.SectionId, model.BackgroundImageId);

                _pageSectionService.SetBackgroundStyle(model.SectionId, model.PageSectionBackgroundStyle);
            }

            _pageSectionService.Height(model.SectionId, model.PageSectionHeight);
            _pageSectionService.Roles(model.SectionId, model.SelectedRoleList);

            return Content("Refresh");
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public ActionResult Add(int pageId)
        {
            var model = new AddViewModel
            {
                PageId = pageId,
                SectionTypeList = _pageSectionTypeService.Get()
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            _pageSectionService.Add(model.PageId, model.PageSectionTypeId);

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

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

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int pageSectionId)
        {
            try
            {
                _pageSectionService.Delete(pageSectionId);

                return Json(new { State = true });
            }
            catch (Exception ex)
            {
                return Json(new { State = false, Message = ex.InnerException.Message });
            }
        }
    }
}