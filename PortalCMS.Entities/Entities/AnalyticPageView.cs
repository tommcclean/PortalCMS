﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Entities.Entities
{
    public class AnalyticPageView
    {
        [Key]
        public int AnalyticPageViewId { get; set; }

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public string UserAgent { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public string Area { get; set; }

        public string ReferredUrl { get; set; }

        public string UserId { get; set; }
    }
}