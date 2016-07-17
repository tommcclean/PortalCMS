using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Analytics
{
    public class AnalyticPageView
    {
        [Key]
        public int AnalyticPageViewId { get; set; }

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public string UserAgent { get; set; }

        public string Area { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }

        public string ReferredUrl { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }
    }
}