using Portal.CMS.Entities;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
	public interface ILoginService
	{
		Task<string> SSOAsync(string userId, string token);
	}

	public class LoginService : ILoginService
	{
		#region Dependencies

		readonly PortalDbContext _context;
		readonly ITokenService _tokenService;

		public LoginService(PortalDbContext context, ITokenService tokenService)
		{
			_context = context;
			_tokenService = tokenService;
		}

		#endregion Dependencies

		public async Task<string> SSOAsync(string userId, string token)
		{
			var tokenResult = await _tokenService.RedeemSSOTokenAsync(userId, token);

			if (string.IsNullOrWhiteSpace(tokenResult))
				return userId;

			return string.Empty;
		}
	}
}