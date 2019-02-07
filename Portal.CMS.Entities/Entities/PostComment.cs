using Portal.CMS.Entities.Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class PostComment
    {
        [Key]
        public int PostCommentId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string CommentBody { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}