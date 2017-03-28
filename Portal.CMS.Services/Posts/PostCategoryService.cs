using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public interface IPostCategoryService
    {
        PostCategory Get(int postCategoryId);

        IEnumerable<PostCategory> Get();

        int Add(string postCategoryName);

        void Edit(int postCategoryId, string postCategoryName);

        void Delete(int postCategoryId);
    }

    public class PostCategoryService : IPostCategoryService
    {
        #region Dependencies

        public PortalEntityModel _context;

        public PostCategoryService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public PostCategory Get(int postCategoryId)
        {
            var postCategory = _context.PostCategories.SingleOrDefault(x => x.PostCategoryId == postCategoryId);

            return postCategory;
        }

        public IEnumerable<PostCategory> Get()
        {
            var results = _context.PostCategories.OrderBy(x => x.PostCategoryName).ToList();

            return results;
        }

        public int Add(string postCategoryName)
        {
            var newPostCategory = new PostCategory
            {
                PostCategoryName = postCategoryName
            };

            _context.PostCategories.Add(newPostCategory);

            _context.SaveChanges();

            return newPostCategory.PostCategoryId;
        }

        public void Edit(int postCategoryId, string postCategoryName)
        {
            var postCategory = _context.PostCategories.SingleOrDefault(x => x.PostCategoryId == postCategoryId);
            if (postCategory == null) return;

            postCategory.PostCategoryName = postCategoryName;

            _context.SaveChanges();
        }

        public void Delete(int postCategoryId)
        {
            var postCategory = _context.PostCategories.SingleOrDefault(x => x.PostCategoryId == postCategoryId);
            if (postCategory == null) return;

            _context.PostCategories.Remove(postCategory);

            _context.SaveChanges();
        }
    }
}