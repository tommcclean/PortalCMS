using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class SingleSignOnController : Controller
	{
		private readonly IRoleService _roleService;
		private readonly IUserService _userService;

		public SingleSignOnController(IRoleService roleService, IUserService userService)
		{
			_roleService = roleService;
			_userService = userService;
		}

		public async Task<ActionResult> Index()
		{
			var resetCookie = Request.Cookies["PortalCMS_SSO"];

			if (!UserHelper.IsLoggedIn && resetCookie != null)
			{
				var cookieValues = resetCookie.Value.Split(',');
				resetCookie.Expires = DateTime.Now.AddDays(-1);
				Response.Cookies.Add(resetCookie);
			}

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}