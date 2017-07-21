using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public interface IPostCommentService
    {
        void Add(int userId, int postId, string commentBody);
    }

    public class PostCommentService : IPostCommentService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public PostCommentService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public void Add(int userId, int postId, string commentBody)
        {
            var comment = new PostComment
            {
                UserId = userId,
                PostId = postId,
                CommentBody = commentBody,
                DateAdded = DateTime.Now
            };

            _context.PostComments.Add(comment);

            _context.SaveChanges();
        }
    }
}