using PortalCMS.Entities;
using PortalCMS.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace PortalCMS.Services.Posts
{
    public interface IPostCommentService
    {
        Task AddAsync(string userId, int postId, string commentBody);
    }

    public class PostCommentService : IPostCommentService
    {
        #region Dependencies

        readonly PortalDbContext _context;

        public PostCommentService(PortalDbContext context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task AddAsync(string userId, int postId, string commentBody)
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