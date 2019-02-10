using PortalCMS.Entities.Enumerators;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Authentication.ViewModels.Login;
using PortalCMS.Web.Areas.Authentication.ViewModels.Recovery;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class RecoveryController : Controller
	{
		private readonly ITokenService _tokenService;

		public RecoveryController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

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
			var token = await _tokenService.AddAsync(model.EmailAddress, UserTokenType.ForgottenPassword);

			if (!string.IsNullOrWhiteSpace(token))
			{
				var websiteName = SettingHelper.Get("Website Name");

				var recoveryLink = $@"http://{System.Web.HttpContext.Current.Request.Url.Authority}{Url.Action(nameof(Reset), "Recovery", new { area = "Authentication", id = token })}";

				await EmailHelper.SendEmailAsync(model.EmailAddress, "Password Reset", $"<p>You submitted a request on {websiteName} for assistance in resetting your password. To change your password please click on the link below and complete the requested information.</p><a href=\"{recoveryLink}\">Recover Account</a>");
			}

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
			if (!string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.ConfirmPassword))
			{
				if (!model.Password.Equals(model.ConfirmPassword, StringComparison.Ordinal))
					ModelState.AddModelError("Confirmation", "The passwords you entered do not match.");
			}

			if (!ModelState.IsValid)
				return View(model);

			var result = await _tokenService.RedeemPasswordTokenAsync(model.Token, model.EmailAddress, model.Password);

			if (!string.IsNullOrWhiteSpace(result))
			{
				ModelState.AddModelError("Execution", result);

				return View(model);
			}

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}