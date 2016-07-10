using Portal.CMS.Entities.Entities.Generic;
using Portal.CMS.Entities.Entities.Posts;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Posts
{
    public class EditPostviewModel
    {
        public int PostId { get; set; }

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
        public PostCategory PostCategory { get; set; }

        [DisplayName("Status")]
        public PublicationState PublicationState { get; set; }

        public int BannerImageId { get; set; }

        public string GalleryImageList { get; set; }

        public List<Image> ImageList { get; set; }

        public List<int> ExistingGalleryImageList { get; set; }
    }
}