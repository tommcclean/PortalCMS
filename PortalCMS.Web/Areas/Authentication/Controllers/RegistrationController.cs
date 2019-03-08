using Microsoft.AspNet.Identity;
using PortalCMS.Library.ExtensionMethods;
using PortalCMS.Library.ReCaptcha;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Authentication.ViewModels.Registration;
using PortalCMS.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class RegistrationController : SignInController
	{
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;
		private readonly IRegistrationService _registrationService;

		public RegistrationController(IUserService userService, IRoleService roleService, IRegistrationService registrationService)
		{
			_userService = userService;
			_roleService = roleService;
			_registrationService = registrationService;
		}

		[HttpGet]
		[OutputCache(Duration = 86400)]
		public ActionResult Index()
		{
			var model = new RegisterViewModel();
			if(string.IsNullOrEmpty(SettingHelper.Get("Recaptcha Site Key")))
			{
				return View("_RegistrationSimpleForm", model);
			}

			return View("_RegistrationForm", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RegisterSimple(RegisterViewModel model)
		{
			if (ModelState.IsValid && string.IsNullOrEmpty(SettingHelper.Get("Recaptcha Site Key")))
			{
				return await ProcessRegistration("_RegistrationSimpleForm", model);
			}

			return View("_RegistrationSimpleForm", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			string captchaResponse = Request.Form["g-Recaptcha-Response"];
			string secretKey = @SettingHelper.Get("Recaptcha Secret Key");

			try
			{
				var result = ReCaptchaValidator.IsValidAsync(captchaResponse, secretKey);
				if (result.Success && ModelState.IsValid && SettingHelper.Get("Recaptcha Site Key").HasValue())
				{
					return await ProcessRegistration("_RegistrationForm", model);
				}

				if (!result.Success)
				{
					foreach (string err in result.ErrorCodes)
					{
						ModelState.AddModelError("", err);
					}
				}
			}
			catch(Exception ex)
			{
				var msg = ex.Message;
			}

			return View("_RegistrationForm", model);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		private async Task<ActionResult> ProcessRegistration(string viewName, RegisterViewModel model)
		{
			// First registration saved will be the administrator
			var isAdministrator = (await _userService.CountAsync() < 1);
			var userId = await _registrationService.RegisterAsync(model.EmailAddress, model.Password, model.GivenName, model.FamilyName, isAdministrator);

			switch (string.IsNullOrEmpty(userId))
			{
				case true:
					ModelState.AddModelError("RegistrationError", "Unable to complete registration - please try again!");
					return View(viewName, model);

				default:
					if (isAdministrator)
					{
						await _roleService.UpdateAsync(userId, new List<string> { nameof(Admin), "Authenticated" });

						var user = UserManager.FindByEmail(model.EmailAddress);
						await SignInManager.PasswordSignInAsync(user.UserName, model.Password, true, shouldLockout: false);
						return Content("Setup");
					}
					else
					{
						await _roleService.UpdateAsync(userId, new List<string> { "Authenticated" });

						// Send email if enabled
						if (EmailHelper.IsEmailEnabled)
						{
							var user = await UserManager.FindByEmailAsync(model.EmailAddress);

							// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
							// Send an email with this link
							string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

							// Create actionlink
							var url = new UrlHelper(HttpContext.Request.RequestContext);
							var callbackUrl = url.Action("ConfirmEmail", "Registration", new { area = "Authentication", userId = user.Id, code }, protocol: Request.Url.Scheme);

							await EmailHelper.SendAccountActivationLinkAsync(user, code, callbackUrl);
						}
					}

					return View("_Welcome");
			}
		}

		//
		// GET: /Account/ConfirmEmail
		[AllowAnonymous]
		public async Task<ActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return View("Error");
			}
			var result = await UserManager.ConfirmEmailAsync(userId, code);
			return View(result.Succeeded ? "_EmailConfirmed" : "Error");
		}
	}
}