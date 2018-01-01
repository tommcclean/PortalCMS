using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Architecture.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Authentication.Controllers
{
    public class SingleSignOnController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public SingleSignOnController(ILoginService loginService, IRoleService roleService, IUserService userService)
        {
            _loginService = loginService;
            _roleService = roleService;
            _userService = userService;
        }

        public async Task<ActionResult> Index()
        {
            var resetCookie = Request.Cookies["PortalCMS_SSO"];

            if (!UserHelper.IsLoggedIn && resetCookie != null)
            {
                var cookieValues = resetCookie.Value.Split(',');

                var result = await _loginService.SSOAsync(Convert.ToInt32(cookieValues[0]), cookieValues[2]);

                if (result.HasValue)
                {
                    Session.Add("UserAccount", await _userService.GetUserAsync(result.Value));
                    Session.Add("UserRoles", await _roleService.GetAsync(result));
                }

                resetCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(resetCookie);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}