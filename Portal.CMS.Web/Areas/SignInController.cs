using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Portal.CMS.Entities.Entities.Models;

namespace Portal.CMS.Web.Controllers.Base
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