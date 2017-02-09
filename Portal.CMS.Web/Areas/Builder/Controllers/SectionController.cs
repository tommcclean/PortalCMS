using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Generic;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Builder.ViewModels.Section;
using Portal.CMS.Web.ViewModels.Shared;
using System;
using System.IO;
using System.Linq;
using System.Web;
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

        private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";

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

            var model = new EditViewModel
            {
                PageId = pageSection.PageId,
                SectionId = sectionId,
                MediaLibrary = new PaginationViewModel
                {
                    ImageList = _imageService.Get(),
                    PaginationType = "section"
                },
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
            if ("upload".Equals(model.BackgroundType, StringComparison.OrdinalIgnoreCase) && model.AttachedImage != null)
            {
                var imageFilePath = SaveImage(model.AttachedImage);

                var uploadedImageId = _imageService.Create(imageFilePath, model.ImageCategory);

                _pageSectionService.SetBackgroundType(model.SectionId, true);

                _pageSectionService.Background(model.SectionId, uploadedImageId);

                _pageSectionService.SetBackgroundStyle(model.SectionId, model.PageSectionBackgroundStyle);
            }
            else if ("colour".Equals(model.BackgroundType, StringComparison.OrdinalIgnoreCase))
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
            _pageSectionService.Markup(model.PageSectionId, model.PageSectionBody);

            return Json(new { State = true, Markup = model.PageSectionBody });
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
                return Json(new { State = false, ex.InnerException.Message });
            }
        }

        private string SaveImage(HttpPostedFileBase imageFile)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToUpper();

            if (extension != ".PNG" && extension != ".JPG" && extension != ".GIF")
                throw new ArgumentException("Unexpected Image Format Provided");

            var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var imageFileName = $"media-{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{imageFile.FileName}";
            var path = Path.Combine(Server.MapPath(IMAGE_DIRECTORY), imageFileName);

            imageFile.SaveAs(path);

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace("Builder/Section/Edit", string.Empty);
            var relativeFilePath = $"{siteURL}{IMAGE_DIRECTORY}/{imageFileName}";

            return relativeFilePath;
        }
    }
}