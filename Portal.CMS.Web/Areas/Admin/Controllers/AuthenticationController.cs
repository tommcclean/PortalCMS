using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
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
        private readonly ITokenService _tokenService;

        private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/Avatars";

        public AuthenticationController(ILoginService loginService, IRegistrationService registrationService, IUserService userService, IRoleService roleService, ITokenService tokenService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
            _userService = userService;
            _roleService = roleService;
            _tokenService = tokenService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Home", new { area = "" });
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

            Session.Add("UserAccount", _userService.GetUser(userId.Value));
            Session.Add("UserRoles", _roleService.Get(userId.Value));

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction(nameof(Index), "Home", new { area = "" });
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

            bool isAdministrator = false;

            var userId = _registrationService.Register(model.EmailAddress, model.Password, model.GivenName, model.FamilyName);

            switch (userId.Value)
            {
                case -1:
                    ModelState.AddModelError("EmailAddressUsed", "The Email Address you entered is already registered");
                    return View("_Register", model);

                default:
                    if (_userService.GetUserCount() == 1)
                    {
                        _roleService.Update(userId.Value, new List<string> { nameof(Admin), "Authenticated" });

                        isAdministrator = true;
                    }
                    else
                    {
                        _roleService.Update(userId.Value, new List<string> { "Authenticated" });
                    }

                    Session.Add("UserAccount", _userService.GetUser(userId.Value));
                    Session.Add("UserRoles", _roleService.Get(userId.Value));

                    if (isAdministrator)
                        return Content("Setup");

                    return Content("Refresh");
            }
        }

        [HttpGet, LoggedInFilter]
        public ActionResult Account()
        {
            var model = new AccountViewModel
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

            _userService.UpdateDetails(UserHelper.UserId.Value, model.EmailAddress, model.GivenName, model.FamilyName);

            var userId = UserHelper.UserId;

            Session.Remove("UserAccount");

            Session.Add("UserAccount", _userService.GetUser(userId.Value));

            return Content("Refresh");
        }

        [HttpGet, LoggedInFilter]
        public ActionResult Avatar()
        {
            var model = new AvatarViewModel();

            return View("_Avatar", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Avatar(AvatarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_Avatar", model);
            }

            var imageFilePath = SaveImage(model.AttachedImage, nameof(Avatar));

            _userService.UpdateAvatar(UserHelper.UserId.Value, imageFilePath);

            var userId = UserHelper.UserId;

            Session.Remove("UserAccount");

            Session.Add("UserAccount", _userService.GetUser(userId.Value));

            return this.Content("Refresh");
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

            _registrationService.ChangePassword(UserHelper.UserId.Value, model.NewPassword);

            var websiteAddress = string.Format(@"http://{0}", System.Web.HttpContext.Current.Request.Url.Authority);

            EmailHelper.Send(
                new List<string> { UserHelper.EmailAddress },
                "Account Notice",
                string.Format("<p>Hello {0}</p><p>We just wanted to let you know that your password was changed at {1}. If you didn't change your password, please let us know", UserHelper.FullName, websiteAddress));

            return Content("Refresh");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(LoginViewModel model)
        {
            var token = _tokenService.Add(model.EmailAddress, Entities.Entities.Authentication.UserTokenType.ForgottenPassword);

            if (!string.IsNullOrWhiteSpace(token))
            {
                var websiteName = SettingHelper.Get("Website Name");

                var recoveryLink = string.Format(@"http://{0}{1}", System.Web.HttpContext.Current.Request.Url.Authority, Url.Action("Reset", "Authentication", new { id = token }));

                EmailHelper.Send(
                    new List<string> { model.EmailAddress },
                    "Password Reset",
                    string.Format("<p>You submitted a request on {0} for assistance in resetting your password. To change your password please click on the link below and complete the requested information.</p><a href=\"{1}\">Recover Account</a>", websiteName, recoveryLink));
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
        public ActionResult Reset(ResetViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                if (!model.Password.Equals(model.ConfirmPassword, StringComparison.Ordinal))
                    ModelState.AddModelError("Confirmation", "The passwords you entered do not match.");
            }

            if (!ModelState.IsValid)
                return View(model);

            var result = _tokenService.RedeemPasswordToken(model.Token, model.EmailAddress, model.Password);

            if (!string.IsNullOrWhiteSpace(result))
            {
                ModelState.AddModelError("Execution", result);

                return View(model);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet, LoggedInFilter]
        public ActionResult Bio()
        {
            var userAccount = _userService.GetUser(UserHelper.UserId.Value);

            var model = new BioViewModel
            {
                Bio = userAccount.Bio
            };

            return View("_Bio", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Bio(BioViewModel model)
        {
            _userService.UpdateBio(UserHelper.UserId.Value, model.Bio);

            return Content("Refresh");
        }

        private string SaveImage(HttpPostedFileBase imageFile, string actionName)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToUpper();

            if (extension != ".PNG" && extension != ".JPG" && extension != ".GIF")
                throw new ArgumentException("Unexpected Image Format Provided");

            var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var imageFileName = string.Format("media-{0}-{1}-{2}", DateTime.Now.ToString("ddMMyyyyHHmmss"), UserHelper.UserId, imageFile.FileName);
            var path = Path.Combine(Server.MapPath(IMAGE_DIRECTORY), imageFileName);

            imageFile.SaveAs(path);

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(string.Format("Admin/Authentication/{0}", actionName), string.Empty);
            var relativeFilePath = string.Format("{0}{1}/{2}", siteURL, IMAGE_DIRECTORY, imageFileName);

            return relativeFilePath;
        }
    }
}