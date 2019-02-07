using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Entities.Models;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.BlogManager.ViewModels.Read
{
    public class BlogViewModel
    {
        public Post CurrentPost { get; set; }

        public List<Post> RecentPosts { get; set; }

        public List<Post> SimiliarPosts { get; set; }

        public ApplicationUser Author { get; set; }
    }
}