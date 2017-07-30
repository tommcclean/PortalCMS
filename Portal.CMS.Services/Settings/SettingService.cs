using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Linq;

namespace Portal.CMS.Services.Settings
{
    public interface ISettingService
    {
        Setting Get(string settingName);

        void Edit(string settingName, string settingValue);
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

        public Setting Get(string settingName)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.SettingName == settingName);

            return setting;
        }

        public void Edit(string settingName, string settingValue)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.SettingName == settingName);
            if (setting == null) return;

            setting.SettingValue = settingValue;

            _context.SaveChanges();
        }
    }
}