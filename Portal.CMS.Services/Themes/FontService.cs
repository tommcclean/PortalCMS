using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Themes
{
    public interface IFontService
    {
        Task<Font> GetAsync(int fontId);

        Task<List<Font>> GetAsync();

        Task<int> CreateAsync(string fontName, string fontType, string fontFilePath);

        Task DeleteAsync(int fontId);
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

        public async Task<Font> GetAsync(int fontId)
        {
            var font = await _context.Fonts.SingleOrDefaultAsync(x => x.FontId == fontId);

            return font;
        }

        public async Task<List<Font>> GetAsync()
        {
            var fontList = await _context.Fonts.OrderBy(x => x.FontName).ToListAsync();

            return fontList;
        }

        public async Task<int> CreateAsync(string fontName, string fontType, string fontFilePath)
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

            await _context.SaveChangesAsync();

            return newFont.FontId;
        }

        public async Task DeleteAsync(int fontId)
        {
            var font = await _context.Fonts.SingleOrDefaultAsync(x => x.FontId == fontId);
            if (font == null) return;

            _context.Fonts.Remove(font);

            await _context.SaveChangesAsync();
        }
    }
}