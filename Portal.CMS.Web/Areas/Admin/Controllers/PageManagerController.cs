using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.PageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class PageManagerController : Controller
    {
        #region Dependencies

        private readonly IPageService _pageService;
        private readonly IRoleService _roleService;
        private readonly ITokenService _tokenService;

        public PageManagerController(IPageService pageService, IRoleService roleService, ITokenService tokenService)
        {
            _pageService = pageService;
            _roleService = roleService;
            _tokenService = tokenService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new PagesViewModel()
            {
                PageList = _pageService.Get(),
                PageAreas = new List<string>()
            };

            foreach (var pageArea in model.PageList.GroupBy(x => x.PageArea))
            {
                if (pageArea.Key == null)
                    continue;

                model.PageAreas.Add(pageArea.Key);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel
            {
                RoleList = _roleService.Get()
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleList = _roleService.Get();
                return View("_Create", model);
            }

            var pageId = _pageService.Add(model.PageName, model.PageArea, model.PageController, model.PageAction);

            _pageService.Roles(pageId, model.SelectedRoleList);

            var token = _tokenService.Add(UserHelper.EmailAddress, Entities.Entities.Authentication.UserTokenType.SSO);

            var cookie = new HttpCookie("PortalCMS_SSO", string.Join(",", UserHelper.UserId, HttpContext.Request.Url.AbsoluteUri, token))
            {
                Expires = DateTime.Now.AddMinutes(5)
            };

            ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            System.Web.HttpRuntime.UnloadAppDomain();

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int pageId)
        {
            var page = _pageService.Get(pageId);

            var model = new EditViewModel()
            {
                PageId = page.PageId,
                PageName = page.PageName,
                PageArea = page.PageArea,
                PageController = page.PageController,
                PageAction = page.PageAction,
                RoleList = _roleService.Get(),
                SelectedRoleList = page.PageRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            var page = _pageService.Get(model.PageId);

            var restartRequired = false;

            if (!string.IsNullOrWhiteSpace(page.PageArea).Equals(string.IsNullOrWhiteSpace(model.PageArea)) || !page.PageController.Equals(model.PageController) || !page.PageAction.Equals(model.PageAction))
                restartRequired = true;

            if (!ModelState.IsValid)
            {
                model.RoleList = _roleService.Get();
                return View("_Edit", model);
            }

            _pageService.Edit(model.PageId, model.PageName, model.PageArea, model.PageController, model.PageAction);

            _pageService.Roles(model.PageId, model.SelectedRoleList);

            // RESET: Routing by Starting the Website.
            if (restartRequired)
                System.Web.HttpRuntime.UnloadAppDomain();

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int pageId)
        {
            _pageService.Delete(pageId);

            return RedirectToAction("Index");
        }
    }
}