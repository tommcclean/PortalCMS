using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Posts;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Posts
{
    public class PostCategoryService
    {
        public PortalEntityModel _context = new PortalEntityModel();

        public IEnumerable<PostCategory> Get()
        {
            var results = _context.PostCategories.OrderBy(x => x.PostCategoryName);

            return results;
        }
    }
}