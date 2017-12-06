using Portal.CMS.Services.Settings;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.SettingManager;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter(ActionFilterResponseType.Page)]
    public class SettingManagerController : Controller
    {
        #region Dependencies

        private readonly ISettingService _settingService;

        public SettingManagerController(ISettingService settingService)
        {
            _settingService = settingService;
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
                CDNAddress = SettingHelper.Get("CDN Address")
            };

            if (string.IsNullOrWhiteSpace(model.EmailFromAddress))
                model.EmailFromAddress = UserHelper.EmailAddress;

            return View("_Setup", model);
        }

        [HttpPost]
        public async Task<ActionResult> Setup(SetupViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Setup", model);

            await _settingService.EditAsync("Website Name", model.WebsiteName);
            Session.Remove("Setting-Website Name");

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

            return Content("Refresh");
        }
    }
}