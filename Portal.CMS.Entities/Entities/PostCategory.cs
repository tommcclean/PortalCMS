using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class PostCategory
    {
        [Key]
        public int PostCategoryId { get; set; }

        [Required]
        public string PostCategoryName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}