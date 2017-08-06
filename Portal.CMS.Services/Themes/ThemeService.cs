using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.PageBuilder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Themes
{
    public interface IThemeService
    {
        Task<CustomTheme> GetAsync(int themeId);

        Task<List<CustomTheme>> GetAsync();

        Task<int> UpsertAsync(int themeId, string themeName, int titleFontId, int textFontId, int largeTitleFontSize, int mediumTitleFontSize, int smallTitleFontSize, int tinyTitleFontSize, int textStandardFontSize, string pageBackgroundColour, string menuBackgroundColour, string menuTextColour);

        Task DeleteAsync(int themeId);

        Task DefaultAsync(int themeId);

        Task<CustomTheme> GetDefaultAsync();
    }

    public class ThemeService : IThemeService
    {
        #region Dependencies

        readonly PortalEntityModel _context;
        readonly IPageService _pageService;

        public ThemeService(PortalEntityModel context, IPageService pageService)
        {
            _context = context;
            _pageService = pageService;
        }

        #endregion Dependencies

        public async Task<CustomTheme> GetDefaultAsync()
        {
            var defaultTheme = await _context.Themes.FirstOrDefaultAsync(x => x.IsDefault == true);

            return defaultTheme;
        }

        public async Task<CustomTheme> GetAsync(int themeId)
        {
            var theme = await _context.Themes.SingleOrDefaultAsync(x => x.ThemeId == themeId);

            return theme;
        }

        public async Task<List<CustomTheme>> GetAsync()
        {
            var results = await _context.Themes.ToListAsync();

            return results.OrderByDescending(x => x.IsDefault).ThenByDescending(x => x.DateUpdated).ToList();
        }

        public async Task<int> UpsertAsync(int themeId, string themeName, int titleFontId, int textFontId, int largeTitleFontSize, int mediumTitleFontSize, int smallTitleFontSize, int tinyTitleFontSize, int textStandardFontSize, string pageBackgroundColour, string menuBackgroundColour, string menuTextColour)
        {
            var existingTheme = await GetAsync(themeId);

            if (existingTheme == null)
            {
                var newTheme = new CustomTheme
                {
                    ThemeName = themeName,
                    TitleFontId = titleFontId,
                    TextFontId = textFontId,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    TitleLargeFontSize = largeTitleFontSize,
                    TitleMediumFontSize = mediumTitleFontSize,
                    TitleSmallFontSize = smallTitleFontSize,
                    TitleTinyFontSize = tinyTitleFontSize,
                    TextStandardFontSize = textStandardFontSize,
                    PageBackgroundColour = pageBackgroundColour,
                    MenuBackgroundColour = menuBackgroundColour,
                    MenuTextColour = menuTextColour,
                    IsDefault = false,
                };

                _context.Themes.Add(newTheme);

                await _context.SaveChangesAsync();

                return newTheme.ThemeId;
            }
            else
            {
                existingTheme.ThemeName = themeName;
                existingTheme.TitleFontId = titleFontId;
                existingTheme.TextFontId = textFontId;
                existingTheme.DateUpdated = DateTime.Now;
                existingTheme.TitleLargeFontSize = largeTitleFontSize;
                existingTheme.TitleMediumFontSize = mediumTitleFontSize;
                existingTheme.TitleSmallFontSize = smallTitleFontSize;
                existingTheme.TitleTinyFontSize = tinyTitleFontSize;
                existingTheme.TextStandardFontSize = textStandardFontSize;
                existingTheme.PageBackgroundColour = pageBackgroundColour;
                existingTheme.MenuBackgroundColour = menuBackgroundColour;
                existingTheme.MenuTextColour = menuTextColour;

                await _context.SaveChangesAsync();

                return existingTheme.ThemeId;
            }
        }

        public async Task DeleteAsync(int themeId)
        {
            var existingTheme = await _context.Themes.SingleOrDefaultAsync(x => x.ThemeId == themeId);
            if (existingTheme == null) return;

            _context.Themes.Remove(existingTheme);

            await _context.SaveChangesAsync();
        }

        public async Task DefaultAsync(int themeId)
        {
            var themes = await GetAsync();

            foreach (var theme in themes)
                if (theme.ThemeId == themeId)
                    theme.IsDefault = true;
                else
                    theme.IsDefault = false;

            await _context.SaveChangesAsync();
        }
    }
}