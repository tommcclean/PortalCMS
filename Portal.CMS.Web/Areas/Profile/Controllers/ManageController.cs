using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Profile.ViewModels.Manage;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Profile.Controllers
{
    public class ManageController : Controller
    {
        #region Manifest Constants

        const string IMAGE_DIRECTORY = "/Areas/Profile/Content/Media/Avatars";
        const string USER_ACCOUNT = "UserAccount";

        #endregion Manifest Constants

        #region Dependencies

        readonly IRegistrationService _registrationService;
        readonly IUserService _userService;

        public ManageController(IRegistrationService registrationService, IUserService userService)
        {
            _registrationService = registrationService;
            _userService = userService;
        }

        #endregion Dependencies

        [HttpGet, LoggedInFilter]
        public ActionResult EditAccount()
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
        public async Task<ActionResult> EditAccount(AccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Account", model);

            await _userService.UpdateDetailsAsync(UserHelper.UserId.Value, model.EmailAddress, model.GivenName, model.FamilyName);

            await ResetUserSessionValueAsync();

            return Content("Refresh");
        }

        [HttpGet, LoggedInFilter]
        public ActionResult EditAvatar()
        {
            var model = new AvatarViewModel();

            return View("_Avatar", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAvatar(AvatarViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Avatar", model);

            var imageFilePath = SaveImage(model.AttachedImage, nameof(EditAvatar));

            await _userService.UpdateAvatarAsync(UserHelper.UserId.Value, imageFilePath);

            await ResetUserSessionValueAsync();

            return Content("Refresh");
        }

        [HttpGet, LoggedInFilter]
        public ActionResult EditBio()
        {
            var model = new BioViewModel
            {
                Bio = UserHelper.Bio
            };

            return View("_Bio", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBio(BioViewModel model)
        {
            await _userService.UpdateBioAsync(UserHelper.UserId.Value, model.Bio);

            await ResetUserSessionValueAsync();

            return Content("Refresh");
        }

        [HttpGet, LoggedInFilter]
        public ActionResult EditPassword()
        {
            var model = new PasswordViewModel();

            return View("_Password", model);
        }

        [HttpPost, LoggedInFilter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPassword(PasswordViewModel model)
        {
            if (ModelState.IsValid && !model.NewPassword.Equals(model.ConfirmPassword, StringComparison.OrdinalIgnoreCase))
                ModelState.AddModelError("NewPasswordMismatch", "Your new password and confirm password do not match...");

            if (!ModelState.IsValid)
            {
                model.NewPassword = string.Empty;
                model.ConfirmPassword = string.Empty;

                return View("_Password", model);
            }

            await _registrationService.ChangePasswordAsync(UserHelper.UserId.Value, model.NewPassword);

            var websiteAddress = $@"http://{System.Web.HttpContext.Current.Request.Url.Authority}";

            await EmailHelper.SendEmailAsync(UserHelper.EmailAddress, "Account Notice", $"<p>Hello {UserHelper.FullName}</p><p>We just wanted to let you know that your password was changed at {websiteAddress}. If you didn't change your password, please let us know");

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

            var imageFileName = $"media-{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{UserHelper.UserId}-{imageFile.FileName}";
            var path = Path.Combine(Server.MapPath(IMAGE_DIRECTORY), imageFileName);

            imageFile.SaveAs(path);

            var siteURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace($"Profile/Manage/{actionName}", string.Empty);
            var relativeFilePath = $"{siteURL}{IMAGE_DIRECTORY}/{imageFileName}";

            return relativeFilePath;
        }

        private async Task ResetUserSessionValueAsync()
        {
            var userId = UserHelper.UserId;

            Session.Remove(USER_ACCOUNT);

            Session.Add(USER_ACCOUNT, await _userService.GetUserAsync(userId.Value));
        }
    }
}