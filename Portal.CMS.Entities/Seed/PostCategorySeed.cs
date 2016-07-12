using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.CMS.Entities.Seed
{
    public class PostCategorySeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.PostCategories.Any(x => x.PostCategoryName == "General"))
            {
                context.PostCategories.Add(new Entities.Posts.PostCategory { PostCategoryName = "General" });
            }

            if (!context.PostCategories.Any(x => x.PostCategoryName == "Blog"))
            {
                context.PostCategories.Add(new Entities.Posts.PostCategory { PostCategoryName = "Blog" });
            }
        }
    }
}
