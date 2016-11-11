using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Portal.CMS.Entities.Entities.PageBuilder;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.DevelopmentManager
{
    public class UpsertComponentTypeViewModel
    {
        [Required]
        [DisplayName("Component Name")]
        public string ComponentTypeName { get; set; }

        [Required]
        [DisplayName("Category")]
        public PageComponentTypeCategory ComponentTypeCategory { get; set; }

        [Required]
        [DisplayName("Component Body (HTML)")]
        public string ComponentTypeBody { get; set; }
    }
}