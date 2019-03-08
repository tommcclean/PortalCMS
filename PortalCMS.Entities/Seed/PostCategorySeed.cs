using PortalCMS.Entities.Entities;
using System.Linq;

namespace PortalCMS.Entities.Seed
{
    public static class PostCategorySeed
    {
        public static void Seed(PortalDbContext context)
        {
            if (!context.PostCategories.Any())
                context.PostCategories.Add(new PostCategory { PostCategoryName = "Blog" });
        }
    }
}