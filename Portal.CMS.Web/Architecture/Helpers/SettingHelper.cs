using Portal.CMS.Services.Settings;
using Portal.CMS.Web.DependencyResolution;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class SettingHelper
    {
        public static string Get(string settingName)
        {
            var validSession = System.Web.HttpContext.Current.Session != null;

            object sessionSetting = null;

            if (validSession)
            {
                sessionSetting = System.Web.HttpContext.Current.Session[$"Setting-{settingName}"];
            }

            if (sessionSetting != null)
            {
                return sessionSetting.ToString();
            }
            else
            {
                var container = IoC.Initialize();

                var settingService = container.GetInstance<SettingService>();

                var setting = AsyncHelpers.RunSync(() => settingService.GetAsync(settingName));

                if (setting == null)
                    return string.Empty;

                if (validSession)
                {
                    System.Web.HttpContext.Current.Session.Add($"Setting-{settingName}", setting.SettingValue);
                }

                return setting.SettingValue;
            }
        }
    }
}