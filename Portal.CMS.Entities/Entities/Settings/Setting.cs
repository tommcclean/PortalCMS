using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Settings
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }

        [Required]
        public string SettingName { get; set; }

        [Required]
        public string SettingValue { get; set; }
    }
}