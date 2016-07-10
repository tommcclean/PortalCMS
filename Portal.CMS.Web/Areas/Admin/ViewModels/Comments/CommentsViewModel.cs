using Portal.CMS.Entities.Entities.Posts;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Comments
{
    public class CommentsViewModel
    {
        public List<PostComment> PostComments { get; set; }
    }
}