using PortalCMS.Entities;
using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Enumerators;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PortalCMS.Services.Posts
{
    public interface IPostImageService
    {
        Task AddAsync(int postId, int imageId, PostImageType postImageType);

        Task RemoveAsync(int postId, PostImageType postImageType);
    }

    public class PostImageService : IPostImageService
    {
        #region Dependencies

        readonly PortalDbContext _context;

        public PostImageService(PortalDbContext context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task AddAsync(int postId, int imageId, PostImageType postImageType)
        {
            var postImage = new PostImage
            {
                PostId = postId,
                PostImageType = postImageType,
                ImageId = imageId
            };

            _context.PostImages.Add(postImage);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int postId, PostImageType postImageType)
        {
            var postImages = await _context.PostImages.Where(x => x.PostId == postId && x.PostImageType == postImageType).ToListAsync();

            foreach (var postImage in postImages)
                _context.PostImages.Remove(postImage);

            await _context.SaveChangesAsync();
        }
    }
}