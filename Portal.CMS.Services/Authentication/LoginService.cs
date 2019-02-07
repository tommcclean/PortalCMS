using Microsoft.AspNet.Identity.Owin;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Models;
using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace Portal.CMS.Services.Authentication
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string emailAddress, string password);

        Task<string> SSOAsync(string userId, string token);
    }

	public class LoginService : ILoginService
	{
		#region Dependencies

		readonly PortalDbContext _context;
		readonly ITokenService _tokenService;

		private ApplicationSignInManager _signInManager;
		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set { _signInManager = value; }
		}

		public LoginService(PortalDbContext context, ITokenService tokenService)
		{
			_context = context;
			_tokenService = tokenService;
		}

		#endregion Dependencies

		public async Task<string> LoginAsync(string emailAddress, string password)
		{
			var result = await SignInManager.PasswordSignInAsync(emailAddress, password, true, shouldLockout: false);
			var userAccount = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
			if (userAccount == null) return string.Empty;

			return userAccount.Id;
		}

		public async Task<string> SSOAsync(string userId, string token)
		{
			var tokenResult = await _tokenService.RedeemSSOTokenAsync(userId, token);

			if (string.IsNullOrWhiteSpace(tokenResult))
				return userId;

			return string.Empty;
		}
	}
}