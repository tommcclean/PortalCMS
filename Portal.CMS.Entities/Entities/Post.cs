using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
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
        public int PostAuthorUserId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }

        [Required]
        [ForeignKey("PostCategory")]
        public int PostCategoryId { get; set; }

        public virtual PostCategory PostCategory { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        public virtual ICollection<PostImage> PostImages { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; }

        public virtual ICollection<PostRole> PostRoles { get; set; }

        public virtual ICollection<AnalyticPostView> PostViews { get; set; }
    }
}