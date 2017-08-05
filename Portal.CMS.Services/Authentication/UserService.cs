using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int userId);

        Task<IEnumerable<User>> GetAsync();

        Task<User> GetAsync(string emailAddress);

        Task<IEnumerable<User>> GetAsync(List<string> roleNames);

        Task UpdateDetailsAsync(int userId, string emailAddress, string givenName, string familyName);

        Task UpdateAvatarAsync(int userId, string avatarImagePath);

        Task UpdateBioAsync(int userId, string bio);

        Task DeleteUserAsync(int userId);

        Task<int> GetUserCountAsync();
    }

    public class UserService : IUserService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public UserService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task<User> GetUserAsync(int userId)
        {
            var result = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            return result;
        }

        public async Task<User> GetAsync(string emailAddress)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress, System.StringComparison.OrdinalIgnoreCase));

            return result;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            var userList = await _context.Users.OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId).ToListAsync();

            return userList;
        }

        public async Task<IEnumerable<User>> GetAsync(List<string> roleNames)
        {
            var results = new List<User>();

            foreach (var user in await _context.Users.ToListAsync())
            {
                foreach (var roleName in roleNames)
                {
                    if (user.Roles.Any(x => x.Role.RoleName == roleName))
                        results.Add(user);
                }
            }

            return results.Distinct().OrderBy(x => x.GivenName).ThenBy(x => x.FamilyName).ThenBy(x => x.UserId);
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task UpdateDetailsAsync(int userId, string emailAddress, string givenName, string familyName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            user.EmailAddress = emailAddress;
            user.GivenName = givenName;
            user.FamilyName = familyName;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAvatarAsync(int userId, string avatarImagePath)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            user.AvatarImagePath = avatarImagePath;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBioAsync(int userId, string bio)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            user.Bio = bio;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}