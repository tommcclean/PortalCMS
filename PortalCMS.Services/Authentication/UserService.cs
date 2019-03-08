using PortalCMS.Entities;
using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Entities.Models;
using PortalCMS.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalCMS.Services.Authentication
{
	public interface IUserService : IRepositoryBase<ApplicationUser>
	{
		Task<IEnumerable<ApplicationUser>> GetAsync();

		Task<ApplicationUser> GetByEmailAsync(string emailAddress);

		Task<IEnumerable<ApplicationUser>> GetByRoleAsync(List<string> roleNames);

		Task UpdateDetailsAsync(string userId, string emailAddress, string givenName, string familyName);

		Task UpdateAvatarAsync(string userId, HttpPostedFileBase avatarImage);

		Task UpdateBioAsync(string userId, string bio);

		Task DeleteUserAsync(string userId);
	}

	public class UserService : ServiceBase<ApplicationUser>, IUserService
	{
		#region Dependencies

		public UserService(PortalDbContext context) : base(context) { }

		#endregion Dependencies

		public async Task<ApplicationUser> GetByEmailAsync(string emailAddress)
		{
			var result = await base.DbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(emailAddress, System.StringComparison.OrdinalIgnoreCase));

			return result;
		}

		public async Task<IEnumerable<ApplicationUser>> GetAsync()
		{
			var userList = await base.DbContext.Users.OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.Id).ToListAsync();

			return userList;
		}

		public async Task<IEnumerable<ApplicationUser>> GetByRoleAsync(List<string> roleNames)
		{
			var results = new List<ApplicationUser>();

			foreach (var user in UserManager.Users)
			{
				foreach (var roleName in roleNames)
				{
					if (await UserManager.IsInRoleAsync(user.Id, roleName))
						results.Add(user);
				}
			}

			return results.Distinct().OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.Id);
		}


		public async Task UpdateDetailsAsync(string userId, string emailAddress, string givenName, string familyName)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

			user.Email = emailAddress;
			user.GivenName = givenName;
			user.FamilyName = familyName;

			await base.DbContext.SaveChangesAsync();
		}

		public async Task UpdateAvatarAsync(string userId, HttpPostedFileBase fileUpload)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

			user.AvatarImage = new FileDetail(fileUpload);

			await base.DbContext.SaveChangesAsync();
		}

		public async Task UpdateBioAsync(string userId, string bio)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

			user.Bio = bio;

			await base.DbContext.SaveChangesAsync();
		}

		public async Task DeleteUserAsync(string userId)
		{
			var user = await base.DbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);

			base.DbContext.Users.Remove(user);

			await base.DbContext.SaveChangesAsync();
		}
	}
}