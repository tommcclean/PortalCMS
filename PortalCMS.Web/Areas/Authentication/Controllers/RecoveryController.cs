using PortalCMS.Entities.Enumerators;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Authentication.ViewModels.Login;
using PortalCMS.Web.Areas.Authentication.ViewModels.Recovery;
using PortalCMS.Web.Controllers.Base;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class RecoveryController : BaseController
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
			var user = await UserManager.FindByNameAsync(model.EmailAddress);
			if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
			{
				// Don't reveal that the user does not exist or is not confirmed
				return Content("Refresh");
			}

			// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
			// Send an email with this link
			string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

			var websiteName = SettingHelper.Get("Website Name");

			var recoveryLink = $@"http://{System.Web.HttpContext.Current.Request.Url.Authority}{Url.Action(nameof(Reset), "Recovery", new { area = "Authentication", id = token })}";

			await EmailHelper.SendEmailAsync(model.EmailAddress, "Password Reset", $"<p>You submitted a request on {websiteName} for assistance in resetting your password. To change your password please click on the link below and complete the requested information.</p><a href=\"{recoveryLink}\">Recover Account</a>");

			return Content("Refresh");
		}

		[HttpGet]
		public ActionResult Reset(string id)
		{
			return View(new ResetViewModel { Token = id });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Reset(ResetViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = await UserManager.FindByNameAsync(model.EmailAddress);
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