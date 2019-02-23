using PortalCMS.Services.Settings;
using PortalCMS.Web.DependencyResolution;
using System;
using System.IO;
using System.Web;

namespace PortalCMS.Web.Architecture.Helpers
{
	public static class SettingHelper
	{
		private const string IMAGE_DIRECTORY = "~/Areas/Admin/Content/Media/";

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

		public static byte[] GetImage(string settingName)
		{
			if (string.IsNullOrEmpty(settingName))
			{
				return null;
			}

			var imagePath = Get(settingName);
			var filePath = HttpContext.Current.Server.MapPath(Path.Combine(IMAGE_DIRECTORY, imagePath));
			if (!File.Exists(filePath)) { return null; }

			FileStream fs = null;

			try
			{
				fs = File.OpenRead(filePath);
				var bytes = new byte[fs.Length];
				fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
				return bytes;
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
					fs.Dispose();
				}
			}
		}
	}
}