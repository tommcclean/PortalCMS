using PortalCMS.Entities.Enumerators;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Authentication.ViewModels.Login;
using PortalCMS.Web.Areas.Authentication.ViewModels.PasswordRecovery;
using PortalCMS.Web.Controllers.Base;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class PasswordRecoveryController : BaseController
	{
		[HttpGet]
		[OutputCache(Duration = 86400)]
		public ActionResult Index()
		{
			return View("_RecoveryForm", new LoginViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Forgot(LoginViewModel model)
		{
			var user = await UserManager.FindByEmailAsync(model.EmailAddress);
			if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
			{
				// Don't reveal that the user does not exist or is not confirmed
				return Content("Refresh");
			}

			// Send email if enabled
			if (EmailHelper.IsEmailEnabled)
			{
				// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
				// Send an email with this link
				string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

				// Create actionlink
				var callbackUrl = Url.Action("Reset", "PasswordRecovery", new { area = "Authentication", userId = user.Id, token }, protocol: Request.Url.Scheme);

				await EmailHelper.SendPasswordResetLinkAsync(user, token, callbackUrl);
			}

			return Content("Refresh");
		}

		[HttpGet]
		public ActionResult Reset(string userId, string token)
		{
			return View(new ResetViewModel { UserId = userId, Token = token });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Reset(ResetViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = await UserManager.FindByIdAsync(model.UserId);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return RedirectToAction("Index", "Home", new { area = "" });
			}

			var result = await UserManager.ResetPasswordAsync(user.Id, model.Token, model.Password);

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}