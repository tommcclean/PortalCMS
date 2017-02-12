using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Menu;
using Portal.CMS.Services.Settings;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.SettingManager;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class SettingManagerController : Controller
    {
        #region Dependencies

        readonly ISettingService _settingService;
        readonly IRoleService _roleService;
        readonly IMenuService _menuService;

        public SettingManagerController(ISettingService settingService, IRoleService roleService, IMenuService menuService)
        {
            _settingService = settingService;
            _roleService = roleService;
            _menuService = menuService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new SettingsViewModel
            {
                SettingList = _settingService.Get(),
                RoleList = _roleService.Get(),
                MenuList = _menuService.Get()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("_Create", new CreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _settingService.Add(model.SettingName, model.SettingValue);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int settingId)
        {
            var setting = _settingService.Get(settingId);

            var model = new EditViewModel
            {
                SettingId = settingId,
                SettingName = setting.SettingName,
                SettingValue = setting.SettingValue
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Edit", model);

            _settingService.Edit(model.SettingId, model.SettingName, model.SettingValue);

            Session.Remove($"Setting-{model.SettingName}");

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int settingId)
        {
            _settingService.Delete(settingId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Setup()
        {
            var model = new SetupViewModel
            {
                WebsiteName = SettingHelper.Get("Website Name"),
                WebsiteDescription = SettingHelper.Get("Description Meta Tag"),
                GoogleAnalyticsId = SettingHelper.Get("Google Analytics Tracking ID"),
                EmailFromAddress = SettingHelper.Get("Email From Address"),
                SendGridUserName = SettingHelper.Get("SendGrid UserName"),
                SendGridPassword = SettingHelper.Get("SendGrid Password")
            };

            if (string.IsNullOrWhiteSpace(model.EmailFromAddress))
                model.EmailFromAddress = UserHelper.EmailAddress;

            return View("_Setup", model);
        }

        [HttpPost]
        public ActionResult Setup(SetupViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Setup", model);

            _settingService.Edit("Website Name", model.WebsiteName);
            Session.Remove("Setting-Website Name");

            _settingService.Edit("Description Meta Tag", model.WebsiteDescription);
            Session.Remove("Setting-Description Meta Tag");

            _settingService.Edit("Google Analytics Tracking ID", model.GoogleAnalyticsId);
            Session.Remove("Setting-Google Analytics Tracking ID");

            _settingService.Edit("Email From Address", model.EmailFromAddress);
            Session.Remove("Setting-Email From Address");

            _settingService.Edit("SendGrid UserName", model.SendGridUserName);
            Session.Remove("Setting-SendGrid UserName");

            _settingService.Edit("SendGrid Password", model.SendGridPassword);
            Session.Remove("Setting-SendGrid Password");

            return Content("Refresh");
        }
    }
}