using Portal.CMS.Entities.Entities.Settings;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public class SettingSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Settings.Any(x => x.SettingName == "Website Name"))
            {
                context.Settings.Add(new Setting() { SettingName = "Website Name", SettingValue = "Portal CMS" });
            }
        }
    }
}