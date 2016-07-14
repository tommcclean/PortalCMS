using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.PostCategories
{
    public class EditViewModel
    {
        public int PostCategoryId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string PostCategoryName { get; set; }
    }
}