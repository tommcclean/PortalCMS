using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
