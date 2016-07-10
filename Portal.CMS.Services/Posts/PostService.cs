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

        List<Post> Get(PostCategory? postCategory);

        Post GetLatest();

        int Create(string postTitle, PostCategory postCategory, string postDescription, string postBody);

        void Edit(int postId, string postTitle, PostCategory postCategory, string postDescription, string postBody);

        void Delete(int postId);

        void Publish(int postId);

        void Draft(int postId);
    }

    public class PostService : IPostService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public PostService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public Post Get(int postId)
        {
            var result = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            return result;
        }

        public List<Post> Get(PostCategory? postCategory)
        {
            List<Post> results;

            if (postCategory.HasValue)
                results = _context.Posts.Where(x => x.PostCategory == postCategory.Value).ToList();
            else
                results = _context.Posts.ToList();

            return results;
        }

        public Post GetLatest()
        {
            var result = _context.Posts.Where(x => x.IsPublished).OrderByDescending(x => x.DateUpdated).FirstOrDefault();

            return result;
        }

        public int Create(string postTitle, PostCategory postCategory, string postDescription, string postBody)
        {
            var post = new Post()
            {
                PostTitle = postTitle,
                PostCategory = postCategory,
                PostDescription = postDescription,
                PostBody = postBody,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            _context.Posts.Add(post);

            _context.SaveChanges();

            return post.PostId;
        }

        public void Edit(int postId, string postTitle, PostCategory postCategory, string postDescription, string postBody)
        {
            var post = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            if (post == null)
                return;

            post.PostTitle = postTitle;
            post.PostCategory = postCategory;
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