using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class CopyItem
    {
        [Key]
        public int CopyId { get; set; }

        [Required]
        public string CopyName { get; set; }

        public string CopyBody { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}