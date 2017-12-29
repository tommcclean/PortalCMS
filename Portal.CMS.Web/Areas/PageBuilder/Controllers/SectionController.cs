using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Extensions;
using Portal.CMS.Web.Areas.PageBuilder.ViewModels.Section;
using Portal.CMS.Web.ViewModels.Shared;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [AdminFilter(ActionFilterResponseType.Modal)]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class SectionController : Controller
    {
        private readonly IPageSectionService _sectionService;
        private readonly IPageAssociationService _associationService;
        private readonly IImageService _imageService;
        private readonly IRoleService _roleService;
        private readonly IPageService _pageService;

        public SectionController(IPageSectionService sectionService, IImageService imageService, IRoleService roleService, IPageAssociationService associationService, IPageService pageService)
        {
            _sectionService = sectionService;
            _imageService = imageService;
            _roleService = roleService;
            _associationService = associationService;
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<ActionResult> Reload(int pageSectionId)
        {
            var pageSection = await _sectionService.GetAsync(pageSectionId);

            return Content(pageSection.PageSectionBody);
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public async Task<ActionResult> Add(int pageId)
        {
            var model = new AddViewModel
            {
                PageId = pageId,
                SectionTypeList = await _sectionService.GetSectionTypesAsync(),
                PartialList = await _associationService.GetAsync()
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Add(int pageId, int pageSectionTypeId, string componentStamp)
        {
            try
            {
                var pageAssociation = await _sectionService.AddAsync(pageId, pageSectionTypeId, componentStamp);

                return Json(new { State = true, pageAssociation.PageSection.PageSectionId, pageAssociation.PageAssociationId });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditOrder(int pageId, string associationList)
        {
            if (associationList != null && associationList.Length > 2)
            {
                await _associationService.EditOrderAsync(pageId, associationList);
            }

            return RedirectToAction("Index", "Page", new { pageId });
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public async Task<ActionResult> Clone(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);

            var model = new CloneViewModel
            {
                PageAssociationId = pageAssociationId,
                PageList = await _pageService.GetAsync()
            };

            var currentPage = model.PageList.FirstOrDefault(x => x.PageId == pageAssociation.PageId);
            model.PageList.Remove(currentPage);

            return View("_Clone", model);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<HttpStatusCodeResult> Clone(int pageAssociationId, int pageId)
        {
            try
            {
                await _associationService.CloneAsync(pageAssociationId, pageId);
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Restore(int pageSectionId)
        {
            var pageSection = await _sectionService.GetAsync(pageSectionId);

            var model = new RestoreViewModel
            {
                PageSectionId = pageSectionId,
                PageSectionBackup = pageSection.PageSectionBackups.ToList()
            };

            return View("_Restore", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Restore(int pageSectionId, int restorePointId)
        {
            var pageMarkup = await _sectionService.RestoreBackupAsync(pageSectionId, restorePointId);

            return Json(new { State = true, Markup = pageMarkup });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBackup(int pageSectionId)
        {
            await _sectionService.BackupAsync(pageSectionId);

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBackup(int restorePointId)
        {
            await _sectionService.DeleteBackupAsync(restorePointId);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> EditMarkup(int pageSectionId)
        {
            var pageSection = await _sectionService.GetAsync(pageSectionId);

            var model = new EditMarkupViewModel
            {
                PageSectionId = pageSectionId,
                PageSectionBody = pageSection.PageSectionBody
            };

            return View("_EditMarkup", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditMarkup(EditMarkupViewModel model)
        {
            await _sectionService.EditMarkupAsync(model.PageSectionId, model.PageSectionBody);

            return Json(new { State = true, Markup = model.PageSectionBody });
        }

        [HttpGet]
        public async Task<ActionResult> EditBackgroundImage(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);
            var pageSection = await _sectionService.GetAsync(pageAssociation.PageSection.PageSectionId);

            var model = new EditBackgroundImageViewModel
            {
                PageAssociationId = pageAssociationId,
                SectionId = pageSection.PageSectionId,
                MediaLibrary = new PaginationViewModel
                {
                    ImageList = await _imageService.GetAsync(),
                    TargetInputField = "BackgroundImageId",
                    PaginationType = "section"
                },
            };

            return PartialView("_EditBackgroundImage", model);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<HttpStatusCodeResult> EditBackgroundImage(EditBackgroundImageViewModel model)
        {
            try
            {
                var selectedBackgroundImage = await _imageService.GetAsync(model.BackgroundImageId);

                await _sectionService.EditBackgroundImageAsync(model.SectionId, selectedBackgroundImage.CDNImagePath(), selectedBackgroundImage.ImageCategory);

                await _sectionService.EditBackgroundTypeAsync(model.SectionId, true);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditBackgroundColour(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);
            var pageSection = await _sectionService.GetAsync(pageAssociation.PageSection.PageSectionId);

            var model = new EditBackgroundColourViewModel
            {
                PageAssociationId = pageAssociationId,
                PageSectionId = pageSection.PageSectionId,
                BackgroundColour = await _sectionService.DetermineBackgroundColourAsync(pageSection.PageSectionId),
            };

            return PartialView("_EditBackgroundColour", model);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<HttpStatusCodeResult> EditBackgroundColour(EditBackgroundColourViewModel model)
        {
            try
            {
                await _sectionService.EditBackgroundTypeAsync(model.PageSectionId, false);
                await _sectionService.EditBackgroundColourAsync(model.PageSectionId, model.BackgroundColour);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}