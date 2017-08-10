using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.UserManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter(ActionFilterResponseType.Page)]
    public class UserManagerController : Controller
    {
        #region Dependencies

        private readonly IUserService _userService;
        private readonly IRegistrationService _registrationService;
        private readonly IRoleService _roleService;

        public UserManagerController(IUserService userService, IRegistrationService registrationService, IRoleService roleService)
        {
            _userService = userService;
            _registrationService = registrationService;
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = new UsersViewModel
            {
                Users = await _userService.GetAsync()
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
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            var userId = await _registrationService.RegisterAsync(model.EmailAddress, model.Password, model.GivenName, model.FamilyName);

            switch (userId.Value)
            {
                case -1:
                    ModelState.AddModelError("EmailAddressUsed", "The Email Address you entered is already registered");
                    return View("_Create", model);

                default:
                    if (await _userService.GetUserCountAsync() == 1)
                        await _roleService.UpdateAsync(userId.Value, new List<string> { nameof(Admin) });
                    else
                        await _roleService.UpdateAsync(userId.Value, new List<string> { "Authenticated" });

                    if (!UserHelper.IsLoggedIn)
                        Session.Add("UserAccount", await _userService.GetUserAsync(userId.Value));

                    return Content("Refresh");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int userId)
        {
            var user = await _userService.GetUserAsync(userId);

            var model = new DetailsViewModel
            {
                UserId = userId,
                EmailAddress = user.EmailAddress,
                GivenName = user.GivenName,
                FamilyName = user.FamilyName,
                DateAdded = user.DateAdded,
                DateUpdated = user.DateUpdated
            };

            return View("_Details", model);
        }

        [HttpPost]
        public async Task<ActionResult> Details(DetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Details", model);

            await _userService.UpdateDetailsAsync(model.UserId, model.EmailAddress, model.GivenName, model.FamilyName);

            if (model.UserId == UserHelper.UserId)
            {
                Session.Remove("UserAccount");

                Session.Add("UserAccount", await _userService.GetUserAsync(model.UserId));
            }

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> Roles(int? userId)
        {
            var model = new RolesViewModel
            {
                UserId = userId.Value,
                RoleList = await _roleService.GetUserAssignableRolesAsync()
            };

            var userRoles = await _roleService.GetAsync(userId);

            foreach (var role in userRoles)
                model.SelectedRoleList.Add(role.RoleName);

            return PartialView("_Roles", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Roles(RolesViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Roles", model);

            await _roleService.UpdateAsync(model.UserId.Value, model.SelectedRoleList);

            if (model.UserId == UserHelper.UserId)
            {
                Session.Remove("UserAccount");
                Session.Remove("UserRoles");

                Session.Add("UserAccount", await _userService.GetUserAsync(model.UserId.Value));
                Session.Add("UserRoles", await _roleService.GetAsync(model.UserId));
            }

            return Content("Refresh");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int userId)
        {
            await _userService.DeleteUserAsync(userId);

            return RedirectToAction(nameof(Index));
        }
    }
}