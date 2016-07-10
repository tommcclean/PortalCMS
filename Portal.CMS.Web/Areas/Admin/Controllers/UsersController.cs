using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Areas.Admin.ActionFilters;
using Portal.CMS.Web.Areas.Admin.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [LoggedInFilter, AdminFilter]
    public class UsersController : Controller
    {
        #region Dependencies

        private readonly UserService _userService;
        private readonly RegistrationService _registrationService;
        private readonly IRoleService _roleService;

        public UsersController(UserService userService, RegistrationService registrationService, RoleService roleService)
        {
            _userService = userService;
            _registrationService = registrationService;
            _roleService = roleService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult Index()
        {
            var model = new UsersViewModel()
            {
                Users = _userService.Get()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateViewModel()
            {
            };

            return View("_Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Create", model);

            var userId = _registrationService.Register(model.EmailAddress, model.Password, model.GivenName, model.FamilyName);

            switch (userId.Value)
            {
                case -1:
                    ModelState.AddModelError("EmailAddressUsed", "The Email Address you entered is already registered");
                    return View("_Create", model);

                default:
                    if (_userService.GetUserCount() == 1)
                        _roleService.Update(userId.Value, new List<string>() { "Admin" });

                    if (!UserHelper.IsLoggedIn)
                        Session.Add("UserAccount", _userService.GetUser(userId.Value));

                    return this.Content("Refresh");
            }
        }

        [HttpGet]
        public ActionResult Details(int userId)
        {
            var user = _userService.GetUser(userId);

            var model = new DetailsViewModel()
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
        public ActionResult Details(DetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Details", model);

            _userService.UpdateUser(model.UserId, model.EmailAddress, model.GivenName, model.FamilyName);

            if (model.UserId == UserHelper.UserId)
            {
                Session.Remove("UserAccount");

                Session.Add("UserAccount", _userService.GetUser(model.UserId));
            }

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Roles(int userId)
        {
            var model = new RolesViewModel()
            {
                UserId = userId,
                RoleList = _roleService.Get()
            };

            var userRoles = _roleService.Get(userId);

            foreach (var role in userRoles)
                model.SelectedRoleList.Add(role.Role.RoleName);

            return PartialView("_Roles", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Roles(RolesViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Roles", model);

            _roleService.Update(model.UserId, model.SelectedRoleList);

            if (model.UserId == UserHelper.UserId)
            {
                Session.Remove("UserAccount");

                Session.Add("UserAccount", _userService.GetUser(model.UserId));
            }

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Delete(int userId)
        {
            var model = new DeleteViewModel()
            {
                UserId = userId
            };

            return View("_Delete", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            _userService.DeleteUser(model.UserId);

            return Content("Refresh");
        }
    }
}