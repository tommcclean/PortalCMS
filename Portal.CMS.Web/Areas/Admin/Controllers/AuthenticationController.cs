using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.Authentication;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Dependencies

        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthenticationController(ILoginService loginService, IRegistrationService registrationService, IUserService userService, IRoleService roleService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
            _userService = userService;
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("_Login", new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Login", model);

            var userId = _loginService.Login(model.EmailAddress, model.Password);

            if (!userId.HasValue)
            {
                ModelState.AddModelError("InvalidCredentials", "Invalid Account Credentials");

                return View("_Login", model);
            }

            var userAccount = _userService.GetUser(userId.Value);

            Session.Add("UserAccount", userAccount);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("_Register", new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Register", model);

            var userId = _registrationService.Register(model.EmailAddress, model.Password, model.GivenName, model.FamilyName);

            switch (userId.Value)
            {
                case -1:
                    ModelState.AddModelError("EmailAddressUsed", "The Email Address you entered is already registered");
                    return View("_Register", model);

                default:
                    if (_userService.GetUserCount() == 1)
                        _roleService.Update(userId.Value, new List<string>() { "Admin", "Authenticated" });
                    else
                        _roleService.Update(userId.Value, new List<string>() { "Authenticated" });

                    var userAccount = _userService.GetUser(userId.Value);

                    Session.Add("UserAccount", userAccount);

                    return this.Content("Refresh");
            }
        }

        [HttpGet, LoggedInFilter]
        public ActionResult Account()
        {
            var model = new AccountViewModel()
            {
                EmailAddress = UserHelper.EmailAddress,
                GivenName = UserHelper.GivenName,
                FamilyName = UserHelper.FamilyName
            };

            return View("_Account", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Account(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_Account", model);
            }

            _userService.UpdateUser(UserHelper.UserId, model.EmailAddress, model.GivenName, model.FamilyName);

            var userId = UserHelper.UserId;

            Session.Remove("UserAccount");

            Session.Add("UserAccount", _userService.GetUser(userId));

            return Content("Refresh");
        }

        [HttpGet, LoggedInFilter]
        public ActionResult Password()
        {
            var model = new PasswordViewModel();

            return View("_Password", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Password(PasswordViewModel model)
        {
            if (ModelState.IsValid && !model.NewPassword.Equals(model.ConfirmPassword, System.StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("NewPasswordMismatch", "Your new password and confirm password do not match...");
            }

            if (!ModelState.IsValid)
            {
                model.NewPassword = string.Empty;
                model.ConfirmPassword = string.Empty;

                return View("_Password", model);
            }

            _registrationService.ChangePassword(UserHelper.UserId, model.NewPassword);

            return Content("Refresh");
        }
    }
}