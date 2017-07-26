using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Themes
{
    public interface IFontService
    {
        Font Get(int fontId);

        List<Font> Get();

        int Create(string fontName, string fontType, string fontFilePath);

        void Delete(int fontId);
    }

    public class FontService : IFontService
    {
        #region Dependencies

        private const string CDN_SETTING_NAME = "CDN Address";

        readonly PortalEntityModel _context;
        readonly ISettingService _settingService;

        public FontService(PortalEntityModel context, ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        #endregion Dependencies

        public Font Get(int fontId)
        {
            var cdnAddress = _settingService.Get(CDN_SETTING_NAME).SettingValue;

            var font = _context.Fonts.SingleOrDefault(x => x.FontId == fontId);

            if (!string.IsNullOrWhiteSpace(cdnAddress))
            {
                font.FontPath = $"{cdnAddress}{font.FontPath}";
            }

            return font;
        }

        public List<Font> Get()
        {
            var fontList = _context.Fonts.OrderBy(x => x.FontName).ToList();

            var cdnAddress = _settingService.Get(CDN_SETTING_NAME).SettingValue;

            if (!string.IsNullOrWhiteSpace(cdnAddress))
            {
                foreach (var font in fontList)
                {
                    font.FontPath = $"{cdnAddress}{font.FontPath}";
                }
            }

            return fontList;
        }

        public int Create(string fontName, string fontType, string fontFilePath)
        {
            var newFont = new Font
            {
                FontName = fontName,
                FontType = fontType,
                FontPath = fontFilePath,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.Fonts.Add(newFont);

            _context.SaveChanges();

            return newFont.FontId;
        }

        public void Delete(int fontId)
        {
            var font = _context.Fonts.SingleOrDefault(x => x.FontId == fontId);
            if (font == null) return;

            _context.Fonts.Remove(font);

            _context.SaveChanges();
        }
    }
}