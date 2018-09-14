using Portal.CMS.Services.Themes;
using Portal.CMS.Web.DependencyResolution;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class ThemeHelper
    {
        public static string GetThemeColour()
        {
            var keyName = "ThemeColour";

            var validSession = System.Web.HttpContext.Current.Session != null;

            object sessionSetting = null;

            if (validSession)
            {
                sessionSetting = System.Web.HttpContext.Current.Session[keyName];
            }

            if (sessionSetting != null)
            {
                return sessionSetting.ToString();
            }
            else
            {
                var container = IoC.Initialize();

                var themeService = container.GetInstance<ThemeService>();

                var theme = AsyncHelpers.RunSync(() => themeService.GetDefaultAsync());

                if (theme == null)
                {
                    return "#FFFFFF";
                }

                if (validSession)
                {
                    System.Web.HttpContext.Current.Session.Add(keyName, theme.MenuBackgroundColour);
                }

                return theme.MenuBackgroundColour;
            }
        }
    }
}