using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.RoleManager;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class RoleManagerController : Controller
    {
        #region Dependencies

        readonly IRoleService _roleService;

        public RoleManagerController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "SettingManager");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel();

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            _roleService.Add(model.RoleName);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Edit(int roleId)
        {
            var role = _roleService.Get(roleId);

            var model = new EditViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Edit", model);

            _roleService.Edit(model.RoleId, model.RoleName);

            return this.Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int roleId)
        {
            _roleService.Delete(roleId);

            return RedirectToAction("Index", "SettingManager");
        }
    }
}