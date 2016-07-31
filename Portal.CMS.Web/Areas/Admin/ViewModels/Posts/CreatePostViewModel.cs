using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Entities.Entities.Generic;
using Portal.CMS.Entities.Entities.Posts;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Posts
{
    public class CreatePostViewModel
    {
        [DisplayName("Headline")]
        [Required]
        public string PostTitle { get; set; }

        [DisplayName("Description")]
        [Required]
        public string PostDescription { get; set; }

        [DisplayName("Body")]
        [Required]
        public string PostBody { get; set; }

        [DisplayName("Category")]
        [Required]
        public int PostCategoryId { get; set; }

        [DisplayName("Author")]
        [Required]
        public int PostAuthorUserId { get; set; }

        [DisplayName("Status")]
        public PublicationState PublicationState { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        #region Hidden Fields

        public int BannerImageId { get; set; }

        public string GalleryImageList { get; set; }

        #endregion Hidden Fields

        #region Enumerable Properties

        public List<Image> ImageList { get; set; }

        public IEnumerable<PostCategory> PostCategoryList { get; set; }

        public IEnumerable<User> UserList { get; set; }

        public List<Role> RoleList { get; set; }

        #endregion Enumerable Properties
    }
}