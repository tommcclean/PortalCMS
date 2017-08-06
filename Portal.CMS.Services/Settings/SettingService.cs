using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Settings
{
    public interface ISettingService
    {
        Task<Setting> GetAsync(string settingName);

        Task EditAsync(string settingName, string settingValue);
    }

    public class SettingService : ISettingService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public SettingService(PortalEntityModel context)
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
            if (setting == null) return;

            setting.SettingValue = settingValue;

            await _context.SaveChangesAsync();
        }
    }
}