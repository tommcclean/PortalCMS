using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Posts;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public interface IPostImageService
    {
        List<PostImage> Get(int postId);

        void Add(int postId, int imageId, PostImageType postImageType);

        void Remove(int postImageId);

        void Wipe(int postId, PostImageType postImageType);
    }

    public class PostImageService : IPostImageService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public PostImageService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public List<PostImage> Get(int postId)
        {
            var results = _context.PostImages.Where(x => x.PostId == postId).OrderBy(x => x.PostImageType).ToList();

            return results;
        }

        public void Add(int postId, int imageId, PostImageType postImageType)
        {
            var postImage = new PostImage()
            {
                PostId = postId,
                PostImageType = postImageType,
                ImageId = imageId
            };

            _context.PostImages.Add(postImage);

            _context.SaveChanges();
        }

        public void Remove(int postImageId)
        {
            var postImage = _context.PostImages.FirstOrDefault(x => x.PostImageId == postImageId);

            if (postImage == null)
                return;

            _context.PostImages.Remove(postImage);

            _context.SaveChanges();
        }

        public void Wipe(int postId, PostImageType postImageType)
        {
            var postImages = _context.PostImages.Where(x => x.PostId == postId && x.PostImageType == postImageType);

            foreach (var postImage in postImages)
            {
                _context.PostImages.Remove(postImage);
            }

            _context.SaveChanges();
        }
    }
}