﻿using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.ActionFilters;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Admin.ViewModels.UserManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Admin.Controllers
{
	[AdminFilter(ActionFilterResponseType.Page)]
	public class UserManagerController : Controller
	{
		#region Dependencies

		private readonly IUserService _userService;
		private readonly IRegistrationService _registrationService;
		private readonly IRoleService _roleService;

		public UserManagerController(IUserService userService, IRegistrationService registrationService, IRoleService roleService)
		{
			_userService = userService;
			_registrationService = registrationService;
			_roleService = roleService;
		}

		#endregion Dependencies

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			var model = new UsersViewModel
			{
				Users = await _userService.GetAsync()
			};

			return View(model);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View("_Create", new CreateViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateViewModel model)
		{
			if (!ModelState.IsValid)
				return View("_Create", model);

			var userId = await _registrationService.RegisterAsync(model.EmailAddress, model.Password, model.GivenName, model.FamilyName);

			switch (string.IsNullOrEmpty(userId))
			{
				case true:
					ModelState.AddModelError("RegistrationError", "Unable to complete registration - please try again!");
					return View("_Create", model);

				default:
					await _roleService.UpdateAsync(userId, new List<string> { "Authenticated" });
					return Content("Refresh");
			}
		}

		[HttpGet]
		public async Task<ActionResult> Details(string userId)
		{
			var user = await _userService.GetAsync(userId);

			var model = new DetailsViewModel
			{
				UserId = userId,
				EmailAddress = user.Email,
				GivenName = user.GivenName,
				FamilyName = user.FamilyName,
				DateAdded = user.RegistrationDate,
				DateUpdated = user.LastUpdatedDate
			};

			return View("_Details", model);
		}

		[HttpPost]
		public async Task<ActionResult> Details(DetailsViewModel model)
		{
			if (!ModelState.IsValid)
				return View("_Details", model);

			await _userService.UpdateDetailsAsync(model.UserId, model.EmailAddress, model.GivenName, model.FamilyName);

			return Content("Refresh");
		}

		[HttpGet]
		public async Task<ActionResult> Roles(string userId)
		{
			var model = new RolesViewModel
			{
				UserId = userId,
				RoleList = await _roleService.GetUserAssignableRolesAsync()
			};

			var userRoles = await _roleService.GetByUserAsync(userId);

			foreach (var role in userRoles)
				model.SelectedRoleList.Add(role);

			return PartialView("_Roles", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Roles(RolesViewModel model)
		{
			if (!ModelState.IsValid)
				return View("_Roles", model);

			await _roleService.UpdateAsync(model.UserId, model.SelectedRoleList);

			return Content("Refresh");
		}

		[HttpGet]
		public async Task<ActionResult> Delete(string userId)
		{
			await _userService.DeleteUserAsync(userId);

			return RedirectToAction(nameof(Index));
		}
	}
}