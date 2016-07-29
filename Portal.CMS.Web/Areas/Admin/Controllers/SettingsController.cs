using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.Menu;
using Portal.CMS.Services.Settings;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Settings;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class SettingsController : Controller
    {
        #region Dependencies

        readonly ISettingService _settingService;
        readonly IRoleService _roleService;
        readonly IMenuService _menuService;

        public SettingsController(ISettingService settingService, IRoleService roleService, IMenuService menuService)
        {
            _settingService = settingService;
            _roleService = roleService;
            _menuService = menuService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new SettingsViewModel()
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

            var model = new EditViewModel()
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

            Session.Remove(string.Format("Setting-{0}", model.SettingName));

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int settingId)
        {
            _settingService.Delete(settingId);

            return RedirectToAction("Index");
        }
    }
}