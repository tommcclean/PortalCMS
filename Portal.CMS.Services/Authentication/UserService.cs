using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
	public interface IUserService : IRepositoryBase<User>
	{
		Task<IEnumerable<User>> GetAsync();

		Task<User> GetByEmailAsync(string emailAddress);

		Task<IEnumerable<User>> GetByRoleAsync(List<string> roleNames);

		Task UpdateDetailsAsync(int userId, string emailAddress, string givenName, string familyName);

		Task UpdateAvatarAsync(int userId, string avatarImagePath);

		Task UpdateBioAsync(int userId, string bio);

		Task DeleteUserAsync(int userId);
	}

	public class UserService : ServiceBase<User>, IUserService
	{
		#region Dependencies

		public UserService(PortalDbContext context) : base(context){ }

		#endregion Dependencies

		public async Task<User> GetByEmailAsync(string emailAddress)
		{
			var result = await base.DbContext.Users.FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress, System.StringComparison.OrdinalIgnoreCase));

			return result;
		}

		public async Task<IEnumerable<User>> GetAsync()
		{
			var userList = await base.DbContext.Users.OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId).ToListAsync();

			return userList;
		}

		public async Task<IEnumerable<User>> GetByRoleAsync(List<string> roleNames)
		{
			var results = new List<User>();

			foreach (var user in await base.DbContext.Users.ToListAsync())
			{
				foreach (var roleName in roleNames)
				{
					if (user.Roles.Any(x => x.Role.RoleName == roleName))
						results.Add(user);
				}
			}

			return results.Distinct().OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId);
		}


		public async Task UpdateDetailsAsync(int userId, string emailAddress, string givenName, string familyName)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.UserId == userId);

			user.EmailAddress = emailAddress;
			user.GivenName = givenName;
			user.FamilyName = familyName;

			await base.DbContext.SaveChangesAsync();
		}

		public async Task UpdateAvatarAsync(int userId, string avatarImagePath)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.UserId == userId);

			user.AvatarImagePath = avatarImagePath;

			await base.DbContext.SaveChangesAsync();
		}

		public async Task UpdateBioAsync(int userId, string bio)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.UserId == userId);

			user.Bio = bio;

			await base.DbContext.SaveChangesAsync();
		}

		public async Task DeleteUserAsync(int userId)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.UserId == userId);

			base.DbContext.Users.Remove(user);

			await base.DbContext.SaveChangesAsync();
		}
	}
}