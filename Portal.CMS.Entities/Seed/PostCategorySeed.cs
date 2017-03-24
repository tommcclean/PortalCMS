using Portal.CMS.Entities.Entities;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class PostCategorySeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.PostCategories.Any())
                context.PostCategories.Add(new PostCategory { PostCategoryName = "Blog" });
        }
    }
}