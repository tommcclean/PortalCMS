using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public interface IPostService
    {
        Post Get(int postId);

        List<Post> Get(string postCategoryName, bool published);

        Post GetLatest();

        int Create(string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody);

        void Edit(int postId, string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody);

        void Delete(int postId);

        void Publish(int postId);

        void Draft(int postId);
    }

    public class PostService : IPostService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PostService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public Post Get(int postId)
        {
            var result = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            return result;
        }

        public List<Post> Get(string postCategoryName, bool published)
        {
            var results = _context.Posts.Where(x => (x.PostCategory.PostCategoryName.Equals(postCategoryName, StringComparison.OrdinalIgnoreCase) || postCategoryName == string.Empty) && (published && x.IsPublished || published == false));

            return results.OrderByDescending(x => x.DateUpdated).ThenByDescending(x => x.PostId).ToList();
        }

        public Post GetLatest()
        {
            var result = _context.Posts.Where(x => x.IsPublished).OrderByDescending(x => x.DateUpdated).FirstOrDefault();

            return result;
        }

        public int Create(string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody)
        {
            var post = new Post()
            {
                PostTitle = postTitle,
                PostCategoryId = postCategoryId,
                PostAuthorUserId = postAuthorUserId,
                PostDescription = postDescription,
                PostBody = postBody,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            _context.Posts.Add(post);

            _context.SaveChanges();

            return post.PostId;
        }

        public void Edit(int postId, string postTitle, int postCategoryId, int postAuthorUserId, string postDescription, string postBody)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == postId);

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

        public void Delete(int postId)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            _context.Posts.Remove(post);

            _context.SaveChanges();
        }

        public void Publish(int postId)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.IsPublished = true;

            _context.SaveChanges();
        }

        public void Draft(int postId)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.IsPublished = false;

            _context.SaveChanges();
        }
    }
}