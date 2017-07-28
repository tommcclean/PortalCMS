using System;
using System.Linq;
using System.Web.Mvc;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Extensions;
using Portal.CMS.Web.Areas.PageBuilder.ViewModels.Section;
using Portal.CMS.Web.ViewModels.Shared;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [AdminFilter(ActionFilterResponseType.Modal)]
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
        public ActionResult Add(int pageId)
        {
            var model = new AddViewModel
            {
                PageId = pageId,
                SectionTypeList = _sectionService.GetSectionTypes(),
                PartialList = _associationService.Get()
            };

            return View("_Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int pageAssociationId)
        {
            try
            {
                _associationService.Delete(pageAssociationId);

                return Json(new { State = true });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSection(int pageId, int pageSectionTypeId, string componentStamp)
        {
            try
            {
                var pageAssociation = _sectionService.Add(pageId, pageSectionTypeId, componentStamp);

                return Json(new { State = true, PageSectionId = pageAssociation.PageSection.PageSectionId, PageAssociationId = pageAssociation.PageAssociationId });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddPartial(int pageId, string areaName, string controllerName, string actionName)
        {
            try
            {
                Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

                Type type = types.Where(t => t.Name == $"{controllerName}Controller").SingleOrDefault();

                if (type != null && type.GetMethod(actionName) != null)
                {
                    _partialService.Add(pageId, areaName, controllerName, actionName);

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
        public ActionResult EditOrder(int pageId, string associationList)
        {
            if (associationList != null && associationList.Length > 2)
                _associationService.EditOrder(pageId, associationList);

            return RedirectToAction("Index", "Page", new { pageId });
        }

        #region Section Edit Methods

        [HttpGet]
        public ActionResult EditSection(int pageAssociationId)
        {
            var pageAssociation = _associationService.Get(pageAssociationId);

            var pageSection = _sectionService.Get(pageAssociation.PageSection.PageSectionId);

            var model = new EditSectionViewModel
            {
                PageAssociationId = pageAssociationId,
                SectionId = pageSection.PageSectionId,
                MediaLibrary = new PaginationViewModel
                {
                    ImageList = _imageService.Get(),
                    TargetInputField = "BackgroundImageId",
                    PaginationType = "section"
                },
                PageSectionHeight = _sectionService.DetermineSectionHeight(pageSection.PageSectionId),
                PageSectionBackgroundStyle = _sectionService.DetermineBackgroundStyle(pageSection.PageSectionId),
                BackgroundType = _sectionService.DetermineBackgroundType(pageSection.PageSectionId),
                BackgroundColour = _sectionService.DetermineBackgroundColour(pageSection.PageSectionId),
                RoleList = _roleService.Get(),
                SelectedRoleList = pageAssociation.PageAssociationRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_EditSection", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditSection(EditSectionViewModel model)
        {
            try
            {
                if ("colour".Equals(model.BackgroundType, StringComparison.OrdinalIgnoreCase))
                {
                    _sectionService.EditBackgroundType(model.SectionId, false);

                    if (!string.IsNullOrWhiteSpace(model.BackgroundColour))
                        _sectionService.EditBackgroundColour(model.SectionId, model.BackgroundColour);
                }
                else
                {
                    _sectionService.EditBackgroundType(model.SectionId, true);

                    if (model.BackgroundImageId > 0)
                    {
                        var selectedBackgroundImage = _imageService.Get(model.BackgroundImageId);

                        _sectionService.EditBackgroundImage(model.SectionId, selectedBackgroundImage.CDNImagePath(), selectedBackgroundImage.ImageCategory);
                    }

                    _sectionService.EditBackgroundStyle(model.SectionId, model.PageSectionBackgroundStyle);
                }

                _sectionService.EditHeight(model.SectionId, model.PageSectionHeight);
                _associationService.EditRoles(model.PageAssociationId, model.SelectedRoleList);

                return Json(new { State = true, SectionMarkup = _sectionService.Get(model.SectionId).PageSectionBody });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public ActionResult EditPartial(int pageAssociationId)
        {
            var pageAssociation = _associationService.Get(pageAssociationId);

            var model = new EditPartialViewModel
            {
                PageAssociationId = pageAssociationId,
                PagePartialId = pageAssociation.PagePartial.PagePartialId,
                RoleList = _roleService.Get(),
                SelectedRoleList = pageAssociation.PageAssociationRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_EditPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPartial(EditPartialViewModel model)
        {
            try
            {
                _associationService.EditRoles(model.PageAssociationId, model.SelectedRoleList);

                return Json(new { State = true });
            }
            catch (Exception)
            {
                return Json(new { State = false });
            }
        }

        [HttpGet]
        public ActionResult Markup(int pageSectionId)
        {
            var pageSection = _sectionService.Get(pageSectionId);

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
        public JsonResult Markup(MarkupViewModel model)
        {
            _sectionService.EditMarkup(model.PageSectionId, model.PageSectionBody);

            return Json(new { State = true, Markup = model.PageSectionBody });
        }

        #endregion Section Edit Methods

        #region Section Backup Methods

        [HttpGet]
        public ActionResult Restore(int pageSectionId)
        {
            var pageSection = _sectionService.Get(pageSectionId);

            var model = new RestoreViewModel
            {
                PageSectionId = pageSectionId,
                PageSectionBackup = pageSection.PageSectionBackups.ToList()
            };

            return View("_Restore", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBackup(int pageSectionId)
        {
            _sectionService.Backup(pageSectionId);

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RestoreBackup(int pageSectionId, int restorePointId)
        {
            var pageMarkup = _sectionService.RestoreBackup(pageSectionId, restorePointId);

            return Json(new { State = true, Markup = pageMarkup });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBackup(int restorePointId)
        {
            _sectionService.DeleteBackup(restorePointId);

            return Content("Refresh");
        }

        #endregion Section Backup Methods

        [HttpGet]
        public ActionResult Clone(int pageAssociationId)
        {
            var pageAssociation = _associationService.Get(pageAssociationId);

            var model = new CloneViewModel
            {
                PageAssociationId = pageAssociationId,
                PageList = _pageService.Get().ToList()
            };

            var currentPage = model.PageList.FirstOrDefault(x => x.PageId == pageAssociation.PageId);
            model.PageList.Remove(currentPage);

            return View("_Clone", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clone(CloneViewModel model)
        {
            _associationService.Clone(model.PageAssociationId, model.PageId);

            return Content("Refresh");
        }
    }
}