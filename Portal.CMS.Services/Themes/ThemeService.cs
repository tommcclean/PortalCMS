using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Themes
{
    public interface IThemeService
    {
        CustomTheme Get(int themeId);

        IEnumerable<CustomTheme> Get();

        int Upsert(int themeId, string themeName, int titleFontId, int textFontId, int largeTitleFontSize, int mediumTitleFontSize, int smallTitleFontSize, int tinyTitleFontSize, int textStandardFontSize, string pageBackgroundColour, string menuBackgroundColour, string menuTextColour);

        void Delete(int themeId);

        void Default(int themeId);

        CustomTheme GetDefault();
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

        public CustomTheme GetDefault()
        {
            var defaultTheme = _context.Themes.FirstOrDefault(x => x.IsDefault == true);

            return defaultTheme;
        }

        public CustomTheme Get(int themeId)
        {
            var theme = _context.Themes.SingleOrDefault(x => x.ThemeId == themeId);

            return theme;
        }

        public IEnumerable<CustomTheme> Get()
        {
            var results = _context.Themes.ToList();

            return results.OrderByDescending(x => x.IsDefault).ThenByDescending(x => x.DateUpdated);
        }

        public int Upsert(int themeId, string themeName, int titleFontId, int textFontId, int largeTitleFontSize, int mediumTitleFontSize, int smallTitleFontSize, int tinyTitleFontSize, int textStandardFontSize, string pageBackgroundColour, string menuBackgroundColour, string menuTextColour)
        {
            var existingTheme = Get(themeId);

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

                _context.SaveChanges();

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

                _context.SaveChanges();

                return existingTheme.ThemeId;
            }
        }

        public void Delete(int themeId)
        {
            var existingTheme = _context.Themes.SingleOrDefault(x => x.ThemeId == themeId);

            if (existingTheme == null)
                return;

            _context.Themes.Remove(existingTheme);

            _context.SaveChanges();
        }

        public void Default(int themeId)
        {
            var themes = Get();

            foreach (var theme in themes)
            {
                if (theme.ThemeId == themeId)
                    theme.IsDefault = true;
                else
                    theme.IsDefault = false;
            }

            _context.SaveChanges();
        }
    }
}