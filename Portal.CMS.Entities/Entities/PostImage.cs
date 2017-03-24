using Portal.CMS.Entities.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class PostImage
    {
        [Key]
        public int PostImageId { get; set; }

        public PostImageType PostImageType { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}