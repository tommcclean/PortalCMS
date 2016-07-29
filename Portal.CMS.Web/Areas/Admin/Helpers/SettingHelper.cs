using Portal.CMS.Services.Settings;
using Portal.CMS.Web.DependencyResolution;
using StructureMap;

namespace Portal.CMS.Web.Areas.Admin.Helpers
{
    public static class SettingHelper
    {
        public static string Get(string settingName)
        {
            var sessionSetting = System.Web.HttpContext.Current.Session[string.Format("Setting-{0}", settingName)];

            if (sessionSetting != null)
            {
                return sessionSetting.ToString();
            }
            else
            {
                var container = IoC.Initialize();

                var settingService = container.GetInstance<SettingService>();

                var setting = settingService.Get(settingName);

                if (setting == null)
                    return string.Empty;

                System.Web.HttpContext.Current.Session.Add(string.Format("Setting-{0}", settingName), setting.SettingValue);

                return setting.SettingValue;
            }
        }
    }
}