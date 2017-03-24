using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
    public class PageSectionBackup
    {
        [Key]
        public int PageSectionBackupId { get; set; }

        [Required]
        [ForeignKey("PageSection")]
        public int PageSectionId { get; set; }

        public string PageSectionBody { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual PageSection PageSection { get; set; }
    }
}