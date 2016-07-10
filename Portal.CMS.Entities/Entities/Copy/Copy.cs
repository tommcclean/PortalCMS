using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Copy
{
    public class Copy
    {
        [Key]
        public int CopyId { get; set; }

        [Required]
        public string CopyName { get; set; }

        [Required]
        public string CopyBody { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}