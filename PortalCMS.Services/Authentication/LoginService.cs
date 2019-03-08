using PortalCMS.Entities;
using System.Threading.Tasks;

namespace PortalCMS.Services.Authentication
{
	public class LoginService
	{
		#region Dependencies

		readonly PortalDbContext _context;

		public LoginService(PortalDbContext context)
		{
			_context = context;
		}

		#endregion Dependencies

		//public async Task<string> SSOAsync(string userId, string token)
		//{
		//	var tokenResult = await _tokenService.RedeemSSOTokenAsync(userId, token);

		//	if (string.IsNullOrWhiteSpace(tokenResult))
		//		return userId;

		//	return string.Empty;
		//}
	}
}