using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PortalCMS.Services.Authentication;
using PortalCMS.Web.Areas.Authentication.ViewModels.Login;
using PortalCMS.Web.Controllers.Base;
using System;
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
				return View("_LoginForm", model);

			var user = UserManager.FindByEmail(model.EmailAddress);
			if (user == null)
			{
				ModelState.AddModelError("InvalidCredentials", "Invalid Account Credentials");
				return View("_LoginForm", model);
			}

			var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
			switch (result)
			{
				case SignInStatus.Success:
					user.LastLoginTime = DateTime.Now;
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

		[HttpGet]
		public ActionResult Logout()
		{
			Session.Clear();

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}