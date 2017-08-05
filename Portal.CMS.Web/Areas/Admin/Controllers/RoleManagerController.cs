using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.RoleManager;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter(ActionFilterResponseType.Page)]
    public class RoleManagerController : Controller
    {
        #region Dependencies

        private readonly IRoleService _roleService;

        public RoleManagerController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Index), "SettingManager");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel();

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            await _roleService.AddAsync(model.RoleName);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int roleId)
        {
            var role = await _roleService.GetAsync(roleId);

            var model = new EditViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };

            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Edit", model);

            await _roleService.EditAsync(model.RoleId, model.RoleName);

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int roleId)
        {
            await _roleService.DeleteAsync(roleId);

            return RedirectToAction(nameof(Index), "SettingManager");
        }
    }
}