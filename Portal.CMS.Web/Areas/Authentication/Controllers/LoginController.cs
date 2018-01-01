using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Areas.Authentication.ViewModels.Login;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Authentication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public LoginController(ILoginService loginService, IUserService userService, IRoleService roleService)
        {
            _loginService = loginService;
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        [OutputCache(Duration = 86400)]
        public ActionResult Index()
        {
            return View("_LoginForm", new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_LoginForm", model);

            var userId = await _loginService.LoginAsync(model.EmailAddress, model.Password);

            if (!userId.HasValue)
            {
                ModelState.AddModelError("InvalidCredentials", "Invalid Account Credentials");

                return View("_LoginForm", model);
            }

            Session.Add("UserAccount", await _userService.GetUserAsync(userId.Value));
            Session.Add("UserRoles", await _roleService.GetAsync(userId));

            return Content("Refresh");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}