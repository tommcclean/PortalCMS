using Portal.CMS.Entities;
using Portal.CMS.Services.Settings;
using StructureMap;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class SettingHelper
    {
        internal static IContainer Initialize()
        {
            return new Container(
                c =>
                {
                    c.AddRegistry<DefaultRegistry>();
                    c.For<PortalEntityModel>().Use<PortalEntityModel>().SelectConstructor(() => new PortalEntityModel());
                }
            );
        }

        public static string Get(string settingName)
        {
            var sessionSetting = System.Web.HttpContext.Current.Session[$"Setting-{settingName}"];

            if (sessionSetting != null)
            {
                return sessionSetting.ToString();
            }
            else
            {
                var container = Initialize();

                var settingService = container.GetInstance<SettingService>();

                var setting = settingService.Get(settingName);

                if (setting == null)
                    return string.Empty;

                System.Web.HttpContext.Current.Session.Add($"Setting-{settingName}", setting.SettingValue);

                return setting.SettingValue;
            }
        }
    }
}