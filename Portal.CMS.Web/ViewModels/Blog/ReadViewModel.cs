using Portal.CMS.Entities.Entities.Posts;
using System.Collections.Generic;

namespace Portal.CMS.Web.ViewModels.Blog
{
    public class ReadViewModel
    {
        public Post CurrentPost { get; set; }

        public List<Post> RecentPosts { get; set; }

        public List<Post> SimiliarPosts { get; set; }
    }
}