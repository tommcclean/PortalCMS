using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
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

        readonly PortalEntityModel _context;

        public FontService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public Font Get(int fontId)
        {
            var results = _context.Fonts.SingleOrDefault(x => x.FontId == fontId);

            return results;
        }

        public List<Font> Get()
        {
            var results = _context.Fonts.OrderBy(x => x.FontName).ToList();

            return results;
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

            if (font == null)
                return;

            _context.Fonts.Remove(font);

            _context.SaveChanges();
        }
    }
}