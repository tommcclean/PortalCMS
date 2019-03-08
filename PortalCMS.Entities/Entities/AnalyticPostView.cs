﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Entities.Entities
{
    public class AnalyticPostView
    {
        [Key]
        public int AnalyticPageViewId { get; set; }

        [Required]
        public string IPAddress { get; set; }

        [Required]
        public string UserAgent { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public string ReferredUrl { get; set; }

        public string UserId { get; set; }
    }
}