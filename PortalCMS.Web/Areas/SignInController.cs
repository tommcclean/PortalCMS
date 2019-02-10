using System.Web;
using Microsoft.AspNet.Identity.Owin;
using PortalCMS.Entities.Entities.Models;

namespace PortalCMS.Web.Controllers.Base
{
	public class SignInController : BaseController
	{
		private ApplicationSignInManager _signInManager;
		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			set
			{
				_signInManager = value;
			}
		}
	}
}