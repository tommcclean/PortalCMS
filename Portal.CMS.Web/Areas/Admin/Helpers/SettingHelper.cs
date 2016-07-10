using Portal.CMS.Services.Settings;
using Portal.CMS.Web.DependencyResolution;
using StructureMap;

namespace Portal.CMS.Web.Areas.Admin.Helpers
{
    public static class SettingHelper
    {
        public static string Get(string settingName)
        {
            IContainer container = IoC.Initialize();

            ISettingService settingService = container.GetInstance<SettingService>();

            var setting = settingService.Get(settingName);

            if (setting == null)
                return string.Empty;
            else
                return setting.SettingValue;
        }
    }
}