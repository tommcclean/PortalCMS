using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.SettingManager
{
    public class EditViewModel
    {
        public int SettingId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string SettingName { get; set; }

        [DisplayName("Value")]
        public string SettingValue { get; set; }
    }
}