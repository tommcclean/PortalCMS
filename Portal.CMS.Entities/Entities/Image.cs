using Portal.CMS.Entities.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public ImageCategory ImageCategory { get; set; }

        public string ImagePath { get; set; }
    }
}