using PortalCMS.Services.Authentication;
using PortalCMS.Web.Architecture.Helpers;
using PortalCMS.Web.Areas.Forms.ViewModels.ContactWidgets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PortalCMS.Web.Areas.Forms.Controllers
{
	[SessionState(SessionStateBehavior.ReadOnly)]
	public class ContactWidgetsController : Controller
	{
		#region Dependencies

		private readonly IUserService _userService;

		public ContactWidgetsController(IUserService userService)
		{
			_userService = userService;
		}

		#endregion Dependencies

		[HttpGet]
		[OutputCache(Duration = 86400)]
		public ActionResult SubmitMessageWidget()
		{
			var model = new SubmitMessageViewModel();

			if (UserHelper.IsLoggedIn)
			{
				model.Name = UserHelper.FullName;
				model.EmailAddress = UserHelper.Email;
			}

			return PartialView("_SubmitMessageWidget", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SubmitMessageWidget(SubmitMessageViewModel model)
		{
			var recipients = await _userService.GetByRoleAsync(new List<string> { nameof(Admin) });

			await EmailHelper.SendContactUsMessageAsync(
					recipients.Select(x => x.Email).First(),
					model.Name,
					model.EmailAddress,
					model.Subject,
					model.Message);

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}