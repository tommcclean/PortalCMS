using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Posts;
using Portal.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public interface IPostService
    {
        Post Read(int? userId, int postId);

        IEnumerable<Post> Read(int? userId, string postCategoryName);

        Post Get(int postId);

        List<Post> Get(string postCategoryName, bool published);

        Post GetLatest();

        int Create(string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody);

        void Edit(int postId, string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody);

        void Edit(int postId, string postBody);

        void Delete(int postId);

        void Publish(int postId);

        void Draft(int postId);

        void Roles(int postId, List<string> roleList);
    }

    public class PostService : IPostService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;
        private readonly IUserService _userService;

        public PostService(PortalEntityModel context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        #endregion Dependencies

        public Post Read(int? userId, int postId)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            var userRoleList = new List<string>();

            if (userId.HasValue)
            {
                var user = _userService.GetUser(userId.Value);

                if (user.Roles.Any(x => x.Role.RoleName == "Admin"))
                    return post;

                userRoleList.AddRange(user.Roles.Select(x => x.Role.RoleName));

                if (userRoleList.Contains(post.PostRoles.SelectMany(x => x.Role.RoleName)))
                    return post;
            }
            else if (!post.PostRoles.Any())
            {
                return post;
            }

            return null;
        }

        public IEnumerable<Post> Read(int? userId, string postCategoryName)
        {
            var userRoleList = new List<string>();
            var isAdministrator = false;

            if (userId.HasValue)
            {
                var user = _userService.GetUser(userId.Value);

                if (user.Roles.Any())
                    userRoleList.AddRange(user.Roles.Select(x => x.Role.RoleName));

                if (user.Roles.Any(x => x.Role.RoleName == "Admin"))
                    isAdministrator = true;
            }

            var postList = _context.Posts.Where(x => postCategoryName == string.Empty || x.PostCategory.PostCategoryName == postCategoryName);

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

            return returnList.Distinct().OrderByDescending(x => x.DateUpdated).ThenByDescending(x => x.DateAdded);
        }

        public Post Get(int postId)
        {
            var result = _context.Posts.Include(x => x.PostComments).Include(x => x.PostImages).Include(x => x.PostCategory).SingleOrDefault(x => x.PostId == postId);

            return result;
        }

        public List<Post> Get(string postCategoryName, bool published)
        {
            var results = _context.Posts.Where(x => (x.PostCategory.PostCategoryName.Equals(postCategoryName, StringComparison.OrdinalIgnoreCase) || postCategoryName == string.Empty) && (published && x.IsPublished || !published));

            return results.OrderByDescending(x => x.DateUpdated).ThenByDescending(x => x.PostId).ToList();
        }

        public Post GetLatest()
        {
            var result = _context.Posts.Include(x => x.PostComments).Include(x => x.PostImages).Include(x => x.PostCategory).Where(x => x.IsPublished).OrderByDescending(x => x.DateUpdated).FirstOrDefault();

            return result;
        }

        public int Create(string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody)
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

            _context.SaveChanges();

            return post.PostId;
        }

        public void Edit(int postId, string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostTitle = postTitle;
            post.PostCategoryId = postCategoryId;
            post.PostAuthorUserId = postAuthorUserId;
            post.PostDescription = postDescription;
            post.PostBody = postBody;
            post.DateUpdated = DateTime.Now;

            _context.SaveChanges();
        }

        public void Edit(int postId, string postBody)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostBody = postBody;
            post.DateUpdated = DateTime.Now;

            _context.SaveChanges();
        }

        public void Delete(int postId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            _context.Posts.Remove(post);

            _context.SaveChanges();
        }

        public void Publish(int postId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.IsPublished = true;

            _context.SaveChanges();
        }

        public void Draft(int postId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.IsPublished = false;

            _context.SaveChanges();
        }

        public void Roles(int postId, List<string> roleList)
        {
            var post = Get(postId);

            if (post == null)
                return;

            var roles = _context.Roles.ToList();

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

            _context.SaveChanges();
        }
    }
}