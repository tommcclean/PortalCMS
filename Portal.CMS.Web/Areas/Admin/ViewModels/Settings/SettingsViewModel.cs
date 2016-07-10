using Portal.CMS.Entities.Entities.Authentication;
using Portal.CMS.Entities.Entities.Settings;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Settings
{
    public class SettingsViewModel
    {
        public IEnumerable<Setting> SettingList { get; set; }

        public IEnumerable<Role> RoleList { get; set; }

        public IEnumerable<Entities.Entities.Menu.Menu> MenuList { get; set; }
    }
}