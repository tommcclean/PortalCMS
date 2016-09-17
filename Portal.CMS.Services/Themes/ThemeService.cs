using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Themes
{
    public interface IThemeService
    {
        Theme Get(int themeId);

        IEnumerable<Theme> Get();

        int Upsert(int themeId, string themeName, int titleFontId, int textFontId);

        void Delete(int themeId);
    }

    public class ThemeService : IThemeService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public ThemeService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public Theme Get(int themeId)
        {
            var theme = _context.Themes.FirstOrDefault(x => x.ThemeId == themeId);

            return theme;
        }

        public IEnumerable<Theme> Get()
        {
            var results = _context.Themes.ToList();

            return results;
        }

        public int Upsert(int themeId, string themeName, int titleFontId, int textFontId)
        {
            var existingTheme = Get(themeId);

            if (existingTheme == null)
            {
                var newTheme = new Theme
                {
                    ThemeName = themeName,
                    TitleFontId = titleFontId,
                    TextFontId = textFontId,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    IsDefault = false,
                };

                _context.Themes.Add(newTheme);

                _context.SaveChanges();

                return newTheme.ThemeId;
            }
            else
            {
                existingTheme.ThemeName = themeName;
                existingTheme.TitleFontId = titleFontId;
                existingTheme.TextFontId = textFontId;
                existingTheme.DateUpdated = DateTime.Now;

                _context.SaveChanges();

                return existingTheme.ThemeId;
            }            
        }

        public void Delete(int themeId)
        {
            var existingTheme = _context.Themes.FirstOrDefault(x => x.ThemeId == themeId);

            if (existingTheme == null)
                return;

            _context.Themes.Remove(existingTheme);

            _context.SaveChanges();
        }
    }
}
