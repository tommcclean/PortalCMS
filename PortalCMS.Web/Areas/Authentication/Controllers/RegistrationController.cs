using Microsoft.AspNet.Identity;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Authentication.ViewModels.Registration;
using PortalCMS.Web.Controllers.Base;
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
			return View("_RegistrationForm", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				//when the view is redisplayed the old value will be used. One workaround is to remove the value from the ModelState.
				ModelState.Remove("HumanCheck.CapImageText");
				ModelState.Remove("HumanCheck.CaptchaCodeText");
				model.HumanCheck = new Library.Captcha();
				return View("_RegistrationForm", model);
			}

			// First registration saved will be the administrator
			var isAdministrator = (await _userService.CountAsync() < 1);
			var userId = await _registrationService.RegisterAsync(model.EmailAddress, model.Password, model.GivenName, model.FamilyName, isAdministrator);

			switch (string.IsNullOrEmpty(userId))
			{
				case true:
					ModelState.AddModelError("RegistrationError", "Unable to complete registration - please try again!");

					//when the view is redisplayed the old value will be used. One workaround is to remove the value from the ModelState.
					ModelState.Remove("HumanCheck.CapImageText");
					ModelState.Remove("HumanCheck.CaptchaCodeText");
					model.HumanCheck = new Library.Captcha();
					return View("_RegistrationForm", model);

				default:
					if (isAdministrator)
					{
						await _roleService.UpdateAsync(userId, new List<string> { nameof(Admin), "Authenticated" });

						var user = UserManager.FindByEmail(model.EmailAddress);
						await SignInManager.PasswordSignInAsync(user.UserName, model.Password, true, shouldLockout: false);

						Session.Add("UserAccount", await _userService.GetAsync(user.Id));
						Session.Add("UserRoles", await _roleService.GetByUserAsync(user.Id));
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