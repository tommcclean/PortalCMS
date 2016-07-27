using Portal.CMS.Entities.Entities.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class SettingSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var settingList = context.Settings.ToList();

            var newSettings = new List<Setting>();

            if (!settingList.Any(x => x.SettingName == "Website Name"))
                newSettings.Add(new Setting { SettingName = "Website Name", SettingValue = "Portal CMS" });

            if (!settingList.Any(x => x.SettingName == "Description Meta Tag"))
                newSettings.Add(new Setting { SettingName = "Description Meta Tag", SettingValue = "Portal CMS is a fully featured content management system with a powerful integrated page builder." });

            if (!settingList.Any(x => x.SettingName == "Google Analytics Tracking ID"))
                newSettings.Add(new Setting { SettingName = "Google Analytics Tracking ID", SettingValue = "" });

            if (!settingList.Any(x => x.SettingName == "Email From Address"))
                newSettings.Add(new Setting { SettingName = "Email From Address", SettingValue = "" });

            if (!settingList.Any(x => x.SettingName == "SendGrid UserName"))
                newSettings.Add(new Setting { SettingName = "SendGrid UserName", SettingValue = "" });

            if (!settingList.Any(x => x.SettingName == "SendGrid Password"))
                newSettings.Add(new Setting { SettingName = "SendGrid Password", SettingValue = "" });

            if (newSettings.Any())
                context.Settings.AddRange(newSettings);
        }
    }
}