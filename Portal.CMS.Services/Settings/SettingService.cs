using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Settings
{
    public interface ISettingService
    {
        IEnumerable<Setting> Get();

        Setting Get(int settingId);

        Setting Get(string settingName);

        int Add(string settingName, string settingValue);

        void Edit(int settingId, string settingName, string settingValue);

        void Edit(string settingName, string settingValue);

        void Delete(int settingId);
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

        public IEnumerable<Setting> Get()
        {
            var results = _context.Settings.OrderBy(x => x.SettingName).ToList();

            return results;
        }

        public Setting Get(int settingId)
        {
            var setting = _context.Settings.SingleOrDefault(x => x.SettingId == settingId);

            return setting;
        }

        public Setting Get(string settingName)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.SettingName == settingName);

            return setting;
        }

        public int Add(string settingName, string settingValue)
        {
            var newSetting = new Setting
            {
                SettingName = settingName,
                SettingValue = settingValue
            };

            _context.Settings.Add(newSetting);

            _context.SaveChanges();

            return newSetting.SettingId;
        }

        public void Edit(int settingId, string settingName, string settingValue)
        {
            var setting = _context.Settings.SingleOrDefault(x => x.SettingId == settingId);
            if (setting == null) return;

            setting.SettingName = settingName;
            setting.SettingValue = settingValue;

            _context.SaveChanges();
        }

        public void Edit(string settingName, string settingValue)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.SettingName == settingName);
            if (setting == null) return;

            setting.SettingValue = settingValue;

            _context.SaveChanges();
        }

        public void Delete(int settingId)
        {
            var setting = _context.Settings.SingleOrDefault(x => x.SettingId == settingId);
            if (setting == null) return;

            _context.Settings.Remove(setting);

            _context.SaveChanges();
        }
    }
}