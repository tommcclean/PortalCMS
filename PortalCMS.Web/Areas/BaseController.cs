using System.Web.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PortalCMS.Entities.Entities.Models;

namespace PortalCMS.Web.Controllers.Base
{
	public class BaseController : Controller
	{
		protected string CurrentUserId
		{
			get
			{
				if (Request.IsAuthenticated)
				{
					return HttpContext.User.Identity.GetUserId();
				}

				return string.Empty;
			}
		}

		protected ApplicationUser CurrentUser
		{
			get
			{
				if (Request.IsAuthenticated)
				{
					return UserManager.FindById(CurrentUserId);
				}

				return null;
			}
		}

		private ApplicationUserManager _userManager;
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			set
			{
				_userManager = value;
			}
		}

		private ApplicationRoleManager _roleManager;
		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
			}
			private set
			{
				_roleManager = value;
			}
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);
		}

		// This method helps to get the error information from the MVC "ModelState".
		// We can not directly send the ModelState to the client in Json. The "ModelState"
		// object has some circular reference that prevents it to be serialized to Json.
		public Dictionary<string, object> GetErrorsFromModelState()
		{
			var errors = new Dictionary<string, object>();
			foreach (var key in ModelState.Keys)
			{
				// Only send the errors to the client.
				if (ModelState[key].Errors.Count > 0)
				{
					errors[key] = ModelState[key].Errors;
				}
			}

			return errors;
		}
	}
}