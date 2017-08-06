using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Posts
{
    public interface IPostService
    {
        Task<Post> ReadAsync(int? userId, int postId);

        Task<List<Post>> ReadAsync(int? userId, string postCategoryName);

        Task<Post> GetAsync(int postId);

        Task<List<Post>> GetAsync(string postCategoryName, bool published);

        Task<Post> GetLatestAsync();

        Task<int> CreateAsync(string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody);

        Task EditAsync(int postId, string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody);

        Task EditAsync(int postId, string postBody);

        Task DescriptionAsync(int postId, string description);

        Task HeadlineAsync(int postId, string headline);

        Task DeleteAsync(int postId);

        Task PublishAsync(int postId);

        Task DraftAsync(int postId);

        Task RolesAsync(int postId, List<string> roleList);
    }

    public class PostService : IPostService
    {
        #region Dependencies

        readonly PortalEntityModel _context;
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public PostService(PortalEntityModel context, IUserService userService, IRoleService roleService)
        {
            _context = context;
            _userService = userService;
            _roleService = roleService;
        }

        #endregion Dependencies

        public async Task<Post> ReadAsync(int? userId, int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId && x.IsPublished);

            var userRoles = await _roleService.GetAsync(userId);

            if (_roleService.Validate(post.PostRoles.Select(x => x.Role), userRoles))
                return post;

            return null;
        }

        public async Task<List<Post>> ReadAsync(int? userId, string postCategoryName)
        {
            var userRoleList = new List<string>();
            var isAdministrator = false;

            if (userId.HasValue)
            {
                var user = await _userService.GetUserAsync(userId.Value);

                if (user.Roles.Any())
                    userRoleList.AddRange(user.Roles.Select(x => x.Role.RoleName));

                if (user.Roles.Any(x => x.Role.RoleName == "Admin"))
                    isAdministrator = true;
            }

            var postList = await _context.Posts.Where(x => (postCategoryName == string.Empty || x.PostCategory.PostCategoryName == postCategoryName) && x.IsPublished).ToListAsync();

            var returnList = new List<Post>();

            foreach (var post in postList)
            {
                if (isAdministrator)
                {
                    returnList.Add(post);
                    continue;
                }

                if (!post.PostRoles.Any())
                {
                    returnList.Add(post);

                    continue;
                }

                if (userRoleList.Contains(post.PostRoles.SelectMany(x => x.Role.RoleName)))
                {
                    returnList.Add(post);

                    continue;
                }
            }

            return returnList.Distinct().OrderByDescending(x => x.DateUpdated).ThenByDescending(x => x.DateAdded).ToList();
        }

        public async Task<Post> GetAsync(int postId)
        {
            var result = await _context.Posts.Include(x => x.PostComments).SingleOrDefaultAsync(x => x.PostId == postId);

            return result;
        }

        public async Task<List<Post>> GetAsync(string postCategoryName, bool published)
        {
            var results = await _context.Posts.Where(x => (x.PostCategory.PostCategoryName.Equals(postCategoryName, StringComparison.OrdinalIgnoreCase) || postCategoryName == string.Empty) && (published && x.IsPublished || !published)).ToListAsync();

            return results.OrderByDescending(x => x.DateUpdated).ThenByDescending(x => x.PostId).ToList();
        }

        public async Task<Post> GetLatestAsync()
        {
            var result = await _context.Posts.Include(x => x.PostComments).Include(x => x.PostImages).Include(x => x.PostCategory).Where(x => x.IsPublished).OrderByDescending(x => x.DateUpdated).FirstOrDefaultAsync();

            return result;
        }

        public async Task<int> CreateAsync(string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody)
        {
            var post = new Post
            {
                PostTitle = postTitle,
                PostCategoryId = postCategoryId,
                PostAuthorUserId = postAuthorUserId,
                PostDescription = postDescription,
                PostBody = postBody,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.Posts.Add(post);

            await _context.SaveChangesAsync();

            return post.PostId;
        }

        public async Task EditAsync(int postId, string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostTitle = postTitle;
            post.PostCategoryId = postCategoryId;
            post.PostAuthorUserId = postAuthorUserId;
            post.PostDescription = postDescription;
            post.PostBody = postBody;
            post.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int postId, string postBody)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostBody = postBody;
            post.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DescriptionAsync(int postId, string description)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostDescription = description;
            post.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task HeadlineAsync(int postId, string headline)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostTitle = headline;
            post.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
        }

        public async Task PublishAsync(int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            post.IsPublished = true;

            await _context.SaveChangesAsync();
        }

        public async Task DraftAsync(int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
                return;

            post.IsPublished = false;

            await _context.SaveChangesAsync();
        }

        public async Task RolesAsync(int postId, List<string> roleList)
        {
            var post = await GetAsync(postId);

            if (post == null)
                return;

            var roles = await _context.Roles.ToListAsync();

            if (post.PostRoles != null)
                foreach (var role in post.PostRoles.ToList())
                    _context.PostRoles.Remove(role);

            foreach (var roleName in roleList)
            {
                var currentRole = roles.FirstOrDefault(x => x.RoleName == roleName);

                if (currentRole == null)
                    continue;

                _context.PostRoles.Add(new PostRole { PostId = postId, RoleId = currentRole.RoleId });
            }

            await _context.SaveChangesAsync();
        }
    }
}