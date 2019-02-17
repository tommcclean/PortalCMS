using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Areas.Authentication.ViewModel.Login;
using PortalCMS.Web.Areas.Authentication.ViewModels.Login;
using PortalCMS.Web.Controllers.Base;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class LoginController : SignInController
	{
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;

		public LoginController(IUserService userService, IRoleService roleService)
		{
			_userService = userService;
			_roleService = roleService;
		}

		[HttpGet]
		[OutputCache(Duration = 86400)]
		public ActionResult Index()
		{
			return View("_LoginForm", new LoginViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl="")
		{
			if (!ModelState.IsValid)
			{
				return View("_LoginForm", model);
			}

			var result = SignInStatus.Failure;
			var user = UserManager.FindByEmail(model.EmailAddress);
			if (user != null)
			{
				result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
			}

			switch (result)
			{
				case SignInStatus.Success:
					user.LastLoginDate = DateTime.Now;
					UserManager.Update(user);

					Session.Add("UserAccount", await _userService.GetAsync(user.Id));
					Session.Add("UserRoles", await _roleService.GetByUserAsync(user.Id));
					return Content("Refresh");

				case SignInStatus.LockedOut:
					return View("_LockoutForm", model);

				case SignInStatus.RequiresVerification:
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });

				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("InvalidCredentials", "Invalid Account Credentials");
					return View("_LoginForm", model);
			}
		}

		//
		// GET: /Login/SendCode
		[AllowAnonymous]
		public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
		{
			var userId = await SignInManager.GetVerifiedUserIdAsync();
			if (userId == null)
			{
				return View("Refresh");
			}
			var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
			var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
			return View("_SendCode", new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
		}

		//
		// POST: /Login/SendCode
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SendCode(SendCodeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			// Generate the token and send it
			if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
			{
				return View("Error");
			}
			return RedirectToAction("_VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
		}

		//
		// GET: /Account/VerifyCode
		[AllowAnonymous]
		public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
		{
			// Require that the user has already logged in via username/password or external login
			if (!await SignInManager.HasBeenVerifiedAsync())
			{
				return View("Error");
			}
			return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
		}

		//
		// POST: /Account/VerifyCode
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// The following code protects for brute force attacks against the two factor codes. 
			// If a user enters incorrect codes for a specified amount of time then the user account 
			// will be locked out for a specified amount of time. 
			// You can configure the account lockout settings in IdentityConfig
			var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(model.ReturnUrl);
				case SignInStatus.LockedOut:
					return View("Lockout");
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Invalid code.");
					return View(model);
			}
		}

		[HttpGet]
		public ActionResult Logout()
		{
			CurrentUser.LastLogoutDate = DateTime.Now;
			UserManager.Update(CurrentUser);
			Session.Clear();

			return RedirectToAction("Index", "Home", new { area = "" });
		}

		#region Private methods

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		#endregion
	}
}