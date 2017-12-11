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
        #region Dependencies

        private readonly IPageSectionService _sectionService;
        private readonly IPagePartialService _partialService;
        private readonly IPageAssociationService _associationService;
        private readonly IImageService _imageService;
        private readonly IRoleService _roleService;
        private readonly IPageService _pageService;

        public SectionController(IPageSectionService sectionService, IImageService imageService, IRoleService roleService, IPagePartialService partialService, IPageAssociationService associationService, IPageService pageService)
        {
            _sectionService = sectionService;
            _imageService = imageService;
            _roleService = roleService;
            _partialService = partialService;
            _associationService = associationService;
            _pageService = pageService;
        }

        #endregion Dependencies

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
        public async Task<JsonResult> AddSection(int pageId, int pageSectionTypeId, string componentStamp)
        {
            try
            {
                var pageAssociation = await _sectionService.AddAsync(pageId, pageSectionTypeId, componentStamp);

                return Json(new { State = true, PageSectionId = pageAssociation.PageSection.PageSectionId, PageAssociationId = pageAssociation.PageAssociationId });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddPartial(int pageId, string areaName, string controllerName, string actionName)
        {
            try
            {
                Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

                Type type = types.Where(t => t.Name == $"{controllerName}Controller").SingleOrDefault();

                if (type != null && type.GetMethod(actionName) != null)
                {
                    await _partialService.AddAsync(pageId, areaName, controllerName, actionName);

                    return Json(new { State = true });
                }

                return Json(new { State = false, Reason = "Invalid" });
            }
            catch (Exception)
            {
                return Json(new { State = false, Reason = "Exception" });
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

        #region Section Edit Methods

        [HttpGet]
        public async Task<ActionResult> EditPartial(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);

            var model = new EditPartialViewModel
            {
                PageAssociationId = pageAssociationId,
                PagePartialId = pageAssociation.PagePartial.PagePartialId,
                RoleList = await _roleService.GetAsync(),
                SelectedRoleList = pageAssociation.PageAssociationRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_EditPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPartial(EditPartialViewModel model)
        {
            try
            {
                await _associationService.EditRolesAsync(model.PageAssociationId, model.SelectedRoleList);

                return Json(new { State = true });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Markup(int pageSectionId)
        {
            var pageSection = await _sectionService.GetAsync(pageSectionId);

            var model = new MarkupViewModel
            {
                PageSectionId = pageSectionId,
                PageSectionBody = pageSection.PageSectionBody
            };

            return View("_Markup", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Markup(MarkupViewModel model)
        {
            await _sectionService.EditMarkupAsync(model.PageSectionId, model.PageSectionBody);

            return Json(new { State = true, Markup = model.PageSectionBody });
        }

        #endregion Section Edit Methods

        #region Section Backup Methods

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
        public async Task<ActionResult> CreateBackup(int pageSectionId)
        {
            await _sectionService.BackupAsync(pageSectionId);

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RestoreBackup(int pageSectionId, int restorePointId)
        {
            var pageMarkup = await _sectionService.RestoreBackupAsync(pageSectionId, restorePointId);

            return Json(new { State = true, Markup = pageMarkup });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBackup(int restorePointId)
        {
            await _sectionService.DeleteBackupAsync(restorePointId);

            return Content("Refresh");
        }

        #endregion Section Backup Methods

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
        public async Task<ActionResult> EditAccess(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);

            var model = new EditAccessViewModel
            {
                PageAssociationId = pageAssociationId,
                RoleList = await _roleService.GetAsync(),
                SelectedRoleList = pageAssociation.PageAssociationRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_EditAccess", model);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<HttpStatusCodeResult> EditAccess(EditAccessViewModel model)
        {
            try
            {
                await _associationService.EditRolesAsync(model.PageAssociationId, model.SelectedRoleList);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
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

                //await _sectionService.EditBackgroundStyleAsync(model.SectionId, model.PageSectionBackgroundStyle);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditSection(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);

            var pageSection = await _sectionService.GetAsync(pageAssociation.PageSection.PageSectionId);

            var model = new EditSectionViewModel
            {
                PageAssociationId = pageAssociationId,
                SectionId = pageSection.PageSectionId,
                MediaLibrary = new PaginationViewModel
                {
                    ImageList = await _imageService.GetAsync(),
                    TargetInputField = "BackgroundImageId",
                    PaginationType = "section"
                },
                PageSectionHeight = await _sectionService.DetermineSectionHeightAsync(pageSection.PageSectionId),
                PageSectionBackgroundStyle = await _sectionService.DetermineBackgroundStyleAsync(pageSection.PageSectionId),
                BackgroundType = await _sectionService.DetermineBackgroundTypeAsync(pageSection.PageSectionId),
                BackgroundColour = await _sectionService.DetermineBackgroundColourAsync(pageSection.PageSectionId),
            };

            return View("_EditSection", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditSection(EditSectionViewModel model)
        {
            try
            {
                if ("colour".Equals(model.BackgroundType, StringComparison.OrdinalIgnoreCase))
                {
                    await _sectionService.EditBackgroundTypeAsync(model.SectionId, false);

                    if (!string.IsNullOrWhiteSpace(model.BackgroundColour))
                        await _sectionService.EditBackgroundColourAsync(model.SectionId, model.BackgroundColour);
                }
                else
                {
                    await _sectionService.EditBackgroundTypeAsync(model.SectionId, true);

                    if (model.BackgroundImageId > 0)
                    {
                        var selectedBackgroundImage = await _imageService.GetAsync(model.BackgroundImageId);

                        await _sectionService.EditBackgroundImageAsync(model.SectionId, selectedBackgroundImage.CDNImagePath(), selectedBackgroundImage.ImageCategory);
                    }

                    await _sectionService.EditBackgroundStyleAsync(model.SectionId, model.PageSectionBackgroundStyle);
                }

                await _sectionService.EditHeightAsync(model.SectionId, model.PageSectionHeight);

                var pageSection = await _sectionService.GetAsync(model.SectionId);

                return Json(new { State = true, SectionMarkup = pageSection.PageSectionBody });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Reload(int pageSectionId)
        {
            var pageSection = await _sectionService.GetAsync(pageSectionId);

            return Content(pageSection.PageSectionBody);
        }
    }
}