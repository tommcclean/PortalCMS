using PortalCMS.Entities.Enumerators;
using PortalCMS.Services.Authentication;
using PortalCMS.Services.PageBuilder;
using PortalCMS.Web.Architecture.ActionFilters;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Admin.ViewModels.PageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Admin.Controllers
{
	[AdminFilter(ActionFilterResponseType.Page)]
	public class PageManagerController : Controller
	{
		#region Dependencies

		private readonly IPageService _pageService;
		private readonly IRoleService _roleService;

		public PageManagerController(IPageService pageService, IRoleService roleService)
		{
			_pageService = pageService;
			_roleService = roleService;
		}

		#endregion Dependencies

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			var model = new PagesViewModel
			{
				PageList = await _pageService.GetAsync(),
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
		public async Task<ActionResult> Create()
		{
			var model = new CreateViewModel
			{
				RoleList = await _roleService.GetAsync()
			};

			return View("_Create", model);
		}

		[HttpPost]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.RoleList = await _roleService.GetAsync();
				return View("_Create", model);
			}

			var pageId = await _pageService.AddAsync(model.PageName, model.PageArea, model.PageController, model.PageAction);

			await _pageService.RolesAsync(pageId, model.SelectedRoleList);

			var cookie = new HttpCookie("PortalCMS_SSO", string.Join(",", UserHelper.Id, HttpContext.Request.Url.AbsoluteUri))
			{
				Expires = DateTime.Now.AddMinutes(5)
			};

			ControllerContext.HttpContext.Response.Cookies.Add(cookie);

			HttpRuntime.UnloadAppDomain();

			return Content("SSO");
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int pageId)
		{
			var page = await _pageService.GetAsync(pageId);

			var model = new EditViewModel
			{
				PageId = page.PageId,
				PageName = page.PageName,
				PageArea = page.PageArea,
				PageController = page.PageController,
				PageAction = page.PageAction,
				RoleList = await _roleService.GetAsync(),
				SelectedRoleList = page.PageRoles.Select(x => x.Role.Name).ToList()
			};

			return View("_Edit", model);
		}

		[HttpPost]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EditViewModel model)
		{
			var page = await _pageService.GetAsync(model.PageId);

			var restartRequired = false || (!string.IsNullOrWhiteSpace(page.PageArea).Equals(string.IsNullOrWhiteSpace(model.PageArea)) || !page.PageController.Equals(model.PageController) || !page.PageAction.Equals(model.PageAction));

			if (!ModelState.IsValid)
			{
				model.RoleList = await _roleService.GetAsync();
				return View("_Edit", model);
			}

			await _pageService.EditAsync(model.PageId, model.PageName, model.PageArea, model.PageController, model.PageAction);

			await _pageService.RolesAsync(model.PageId, model.SelectedRoleList);

			// RESET: Routing by Starting the Website.
			if (restartRequired)
				HttpRuntime.UnloadAppDomain();

			return Content("Refresh");
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int pageId)
		{
			await _pageService.DeleteAsync(pageId);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<ActionResult> AppDrawer()
		{
			var model = new AppDrawerViewModel
			{
				PageList = await _pageService.GetAsync()
			};

			return PartialView("_AppDrawer", model);
		}
	}
}