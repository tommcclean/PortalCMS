using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Entities.Entities.Posts;
using Portal.CMS.Entities.Entities.Themes;
using System.Collections.Generic;

namespace Portal.CMS.Web.ViewModels.Blog
{
    public class BlogViewModel
    {
        public Theme Theme { get; set; }

        public Post CurrentPost { get; set; }

        public List<Post> RecentPosts { get; set; }

        public List<Post> SimiliarPosts { get; set; }

        public User Author { get; set; }
    }
}