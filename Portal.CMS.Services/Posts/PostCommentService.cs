using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Posts
{
    public interface IPostCommentService
    {
        Task AddAsync(int userId, int postId, string commentBody);
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

        public async Task AddAsync(int userId, int postId, string commentBody)
        {
            var comment = new PostComment
            {
                UserId = userId,
                PostId = postId,
                CommentBody = commentBody,
                DateAdded = DateTime.Now
            };

            _context.PostComments.Add(comment);

            await _context.SaveChangesAsync();
        }
    }
}