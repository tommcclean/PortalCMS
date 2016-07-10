using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Posts
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string PostTitle { get; set; }

        [Required]
        public string PostDescription { get; set; }

        [Required]
        public string PostBody { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }

        [Required]
        public PostCategory PostCategory { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        public virtual ICollection<PostImage> PostImages { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}