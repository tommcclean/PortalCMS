using PortalCMS.Entities.Enumerators;
using PortalCMS.Services.Generic;
using PortalCMS.Services.Settings;
using PortalCMS.Web.Architecture.ActionFilters;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Admin.ViewModels.SettingManager;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Admin.Controllers
{
	[AdminFilter(ActionFilterResponseType.Page)]
	public class SettingManagerController : Controller
	{
		private const string IMAGE_DIRECTORY = "/Areas/Admin/Content/Media/";

		#region Dependencies

		private readonly ISettingService _settingService;
		private readonly IImageService _imageService;

		public SettingManagerController(ISettingService settingService, IImageService imageService)
		{
			_settingService = settingService;
			_imageService = imageService;
		}

		#endregion Dependencies

		[HttpGet]
		public ActionResult Setup()
		{
			var model = new SetupViewModel
			{
				WebsiteName = SettingHelper.Get("Website Name"),
				WebsiteDescription = SettingHelper.Get("Description Meta Tag"),
				GoogleAnalyticsId = SettingHelper.Get("Google Analytics Tracking ID"),
				EmailFromAddress = SettingHelper.Get("Email From Address"),
				SendGridApiKey = SettingHelper.Get("SendGrid ApiKey"),
				CDNAddress = SettingHelper.Get("CDN Address"),
				RecaptchaSiteKey = SettingHelper.Get("Recaptcha Site Key"),
				RecaptchaSecretKey = SettingHelper.Get("Recaptcha Secret Key"),
			};

			if (string.IsNullOrWhiteSpace(model.EmailFromAddress))
				model.EmailFromAddress = UserHelper.Email;

			return View("_Setup", model);
		}

		[HttpPost]
		public async Task<ActionResult> Setup(SetupViewModel model, HttpPostedFileBase AttachedLogoImage, HttpPostedFileBase AttachedIconImage)
		{
			if (!ModelState.IsValid)
				return View("_Setup", model);

			await _settingService.EditAsync("Website Name", model.WebsiteName);
			Session.Remove("Setting-Website Name");

			if (AttachedLogoImage != null)
			{
				var imageFilePath = SaveImage(AttachedLogoImage);
				await _imageService.CreateAsync(imageFilePath, ImageCategory.Logo);

				await _settingService.EditAsync("Website Logo", imageFilePath);
				Session.Remove("Setting-Website Logo");
			}

			if (AttachedIconImage != null)
			{
				var imageFilePath = SaveImage(AttachedIconImage);
				await _imageService.CreateAsync(imageFilePath, ImageCategory.Logo);

				await _settingService.EditAsync("Website FavIcon", imageFilePath);
				Session.Remove("Setting-Website FavIcon");
			}

			await _settingService.EditAsync("Description Meta Tag", model.WebsiteDescription);
			Session.Remove("Setting-Description Meta Tag");

			await _settingService.EditAsync("Google Analytics Tracking ID", model.GoogleAnalyticsId);
			Session.Remove("Setting-Google Analytics Tracking ID");

			await _settingService.EditAsync("Email From Address", model.EmailFromAddress);
			Session.Remove("Setting-Email From Address");

			await _settingService.EditAsync("SendGrid ApiKey", model.SendGridApiKey);
			Session.Remove("Setting-SendGrid ApiKey");

			await _settingService.EditAsync("CDN Address", model.CDNAddress);
			Session.Remove("Setting-CDN Address");

			await _settingService.EditAsync("Recaptcha Site Key", model.RecaptchaSiteKey);
			Session.Remove("Setting-Recaptcha Site Key");

			await _settingService.EditAsync("Recaptcha Secret Key", model.RecaptchaSecretKey);
			Session.Remove("Setting-Recaptcha Secret Key");

			return Content("Refresh");
		}

		#region private methods

		private string SaveImage(HttpPostedFileBase imageFile)
		{
			var extension = Path.GetExtension(imageFile.FileName).ToUpper();

			if (extension != ".PNG" && extension != ".JPG" && extension != ".GIF")
				throw new ArgumentException("Unexpected Image Format Provided");

			var destinationDirectory = Path.Combine(Server.MapPath(IMAGE_DIRECTORY));

			if (!Directory.Exists(destinationDirectory))
				Directory.CreateDirectory(destinationDirectory);

			var imageFileName = $"media-{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{imageFile.FileName}";
			var path = Path.Combine(Server.MapPath(IMAGE_DIRECTORY), imageFileName);

			imageFile.SaveAs(path);

			var relativeFilePath = $"{IMAGE_DIRECTORY}/{imageFileName}";

			return relativeFilePath;
		}

		#endregion
	}
}