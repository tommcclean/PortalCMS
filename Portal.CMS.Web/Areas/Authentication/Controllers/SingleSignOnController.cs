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

                var result = await _loginService.SSOAsync(cookieValues[0], cookieValues[2]);

                if (!string.IsNullOrEmpty(result))
                {
                    Session.Add("UserAccount", await _userService.GetAsync(result));
                    Session.Add("UserRoles", await _roleService.GetAsync(result));
                }

                resetCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(resetCookie);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}