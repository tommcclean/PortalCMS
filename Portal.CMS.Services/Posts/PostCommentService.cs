using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public interface IPostCommentService
    {
        void Add(int userId, int postId, string commentBody);

        IEnumerable<PostComment> Get();

        IEnumerable<PostComment> Latest();
    }

    public class PostCommentService : IPostCommentService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public PostCommentService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public void Add(int userId, int postId, string commentBody)
        {
            var comment = new PostComment()
            {
                UserId = userId,
                PostId = postId,
                CommentBody = commentBody,
                DateAdded = DateTime.Now,
            };

            _context.PostComments.Add(comment);

            _context.SaveChanges();
        }

        public IEnumerable<PostComment> Get()
        {
            var comments = _context.PostComments.OrderBy(x => x.DateAdded);

            return comments;
        }

        public IEnumerable<PostComment> Latest()
        {
            var results = _context.PostComments.OrderByDescending(x => x.DateAdded).Take(2);

            return results;
        }
    }
}