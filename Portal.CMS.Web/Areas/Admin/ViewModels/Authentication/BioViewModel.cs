using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Authentication
{
    public class BioViewModel
    {
        [DisplayName("Bio")]
        public string Bio { get; set; }
    }
}