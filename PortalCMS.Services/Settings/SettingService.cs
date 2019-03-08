﻿using PortalCMS.Entities;
using PortalCMS.Entities.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PortalCMS.Services.Settings
{
    public interface ISettingService
    {
        Task<Setting> GetAsync(string settingName);

        Task EditAsync(string settingName, string settingValue);
    }

	public class SettingService : ISettingService
	{
		#region Dependencies

		readonly PortalDbContext _context;

		public SettingService(PortalDbContext context)
		{
			_context = context;
		}

		#endregion Dependencies

		public async Task<Setting> GetAsync(string settingName)
		{
			var setting = await _context.Settings.FirstOrDefaultAsync(x => x.SettingName == settingName);

			return setting;
		}

		public async Task EditAsync(string settingName, string settingValue)
		{
			var setting = await _context.Settings.FirstOrDefaultAsync(x => x.SettingName == settingName);
			if (setting == null)
			{
				_context.Settings.Add(new Setting { SettingName = settingName, SettingValue = settingValue });
			}
			else
			{
				setting.SettingValue = settingValue;
			}

			await _context.SaveChangesAsync();
		}
	}
}