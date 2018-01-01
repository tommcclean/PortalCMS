using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Posts
{
    public interface IPostCategoryService
    {
        Task<PostCategory> GetAsync(int postCategoryId);

        Task<IEnumerable<PostCategory>> GetAsync();

        Task<int> AddAsync(string postCategoryName);

        Task EditAsync(int postCategoryId, string postCategoryName);

        Task DeleteAsync(int postCategoryId);
    }

    public class PostCategoryService : IPostCategoryService
    {
        private PortalEntityModel _context;

        public PortalEntityModel context { get => _context; set => _context = value; }

        public PostCategoryService(PortalEntityModel context)
        {
            this.context = context;
        }

        public async Task<PostCategory> GetAsync(int postCategoryId)
        {
            var postCategory = await context.PostCategories.SingleOrDefaultAsync(x => x.PostCategoryId == postCategoryId);

            return postCategory;
        }

        public async Task<IEnumerable<PostCategory>> GetAsync()
        {
            var results = await context.PostCategories.OrderBy(x => x.PostCategoryName).ToListAsync();

            return results;
        }

        public async Task<int> AddAsync(string postCategoryName)
        {
            var newPostCategory = new PostCategory
            {
                PostCategoryName = postCategoryName
            };

            context.PostCategories.Add(newPostCategory);

            await context.SaveChangesAsync();

            return newPostCategory.PostCategoryId;
        }

        public async Task EditAsync(int postCategoryId, string postCategoryName)
        {
            var postCategory = await context.PostCategories.SingleOrDefaultAsync(x => x.PostCategoryId == postCategoryId);
            if (postCategory == null) return;

            postCategory.PostCategoryName = postCategoryName;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int postCategoryId)
        {
            var postCategory = await context.PostCategories.SingleOrDefaultAsync(x => x.PostCategoryId == postCategoryId);
            if (postCategory == null) return;

            context.PostCategories.Remove(postCategory);

            await context.SaveChangesAsync();
        }
    }
}