using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Analytics
{
    public class AnalyticPageView
    {
        [Key]
        public int AnalyticPageViewId { get; set; }

        public string Area { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string ReferredUrl { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }
    }
}