using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using Portal.CMS.Services.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageSectionService
    {
        Task<PageSection> GetAsync(int pageSectionId);

        Task<List<PageSectionType>> GetSectionTypesAsync();

        Task<PageAssociation> AddAsync(int pageId, int pageSectionTypeId, string componentStamp);

        Task EditBackgroundImageAsync(int pageSectionId, string imagePath, ImageCategory imageCategory);

        Task EditBackgroundColourAsync(int pageSectionId, string backgroundColour);

        Task EditHeightAsync(int pageSectionId, PageSectionHeight height);

        Task EditBackgroundStyleAsync(int pageSectionId, PageSectionBackgroundStyle backgroundType);

        Task EditBackgroundTypeAsync(int pageSectionId, bool isPicture);

        Task EditMarkupAsync(int pageSectionId, string htmlBody);

        Task EditAnimationAsync(int pageSectionId, string elementId, string animation);

        Task<PageSectionHeight> DetermineSectionHeightAsync(int pageSectionId);

        Task<PageSectionBackgroundStyle> DetermineBackgroundStyleAsync(int pageSectionId);

        Task<string> DetermineBackgroundTypeAsync(int pageSectionId);

        Task<string> DetermineBackgroundColourAsync(int pageSectionId);

        Task BackupAsync(int pageSectionId);

        Task<string> RestoreBackupAsync(int pageSectionId, int backupId);

        Task DeleteBackupAsync(int backupId);
    }

    public class PageSectionService : IPageSectionService
    {
        private readonly PortalEntityModel _context;

        public PageSectionService(PortalEntityModel context)
        {
            _context = context;
        }

        public async Task<List<PageSectionType>> GetSectionTypesAsync()
        {
            var results = await _context.PageSectionTypes.OrderBy(x => x.PageSectionTypeId).ToListAsync();

            return results;
        }

        public async Task<PageSection> GetAsync(int pageSectionId)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);

            return pageSection;
        }

        public async Task<PageAssociation> AddAsync(int pageId, int pageSectionTypeId, string componentStamp)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId == pageId);
            if (page == null) return null;

            var sectionType = await _context.PageSectionTypes.SingleOrDefaultAsync(x => x.PageSectionTypeId == pageSectionTypeId);
            if (sectionType == null) return null;

            var sectionPosition = 1;

            if (page.PageAssociations.Any())
                sectionPosition = page.PageAssociations.Max(x => x.PageAssociationOrder) + 1;

            var newPageAssociation = new PageAssociation
            {
                PageId = pageId,
                PageSection = new PageSection
                {
                    PageSectionBody = sectionType.PageSectionTypeBody,
                },
                PageAssociationOrder = sectionPosition
            };

            _context.PageAssociations.Add(newPageAssociation);

            await _context.SaveChangesAsync();

            newPageAssociation.PageSection.PageSectionBody = Document.ReplaceTokens(newPageAssociation.PageSection.PageSectionBody, newPageAssociation.PageSection.PageSectionId, componentStamp);

            await _context.SaveChangesAsync();

            return newPageAssociation;
        }

        public async Task EditBackgroundImageAsync(int pageSectionId, string imagePath, ImageCategory imageCategory)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", string.Format("background-image: url('{0}');", imagePath), true);

            if (imageCategory == ImageCategory.Texture)
                document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", "background-size: initial;", false);
            else
                document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", "background-size: cover;", false);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditBackgroundColourAsync(int pageSectionId, string backgroundColour)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", string.Format("background-color: {0};", backgroundColour), true);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditHeightAsync(int pageSectionId, PageSectionHeight height)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateSectionHeight(string.Format("section-{0}", pageSectionId), height);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditBackgroundStyleAsync(int pageSectionId, PageSectionBackgroundStyle backgroundType)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateBackgroundStyle(string.Format("section-{0}", pageSectionId), backgroundType);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditBackgroundTypeAsync(int pageSectionId, bool isPicture)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateBackgroundType(string.Format("section-{0}", pageSectionId), isPicture);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditAnimationAsync(int pageSectionId, string elementId, string animation)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateAnimation(elementId, animation);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditMarkupAsync(int pageSectionId, string htmlBody)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            pageSection.PageSectionBody = htmlBody;

            await _context.SaveChangesAsync();
        }

        public async Task<PageSectionHeight> DetermineSectionHeightAsync(int pageSectionId)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return PageSectionHeight.Tall;

            if (pageSection.PageSectionBody.Contains("height-tall"))
                return PageSectionHeight.Tall;

            if (pageSection.PageSectionBody.Contains("height-medium"))
                return PageSectionHeight.Medium;

            if (pageSection.PageSectionBody.Contains("height-small"))
                return PageSectionHeight.Small;

            if (pageSection.PageSectionBody.Contains("height-standard"))
                return PageSectionHeight.Standard;

            return pageSection.PageSectionBody.Contains("height-tiny") ? PageSectionHeight.Tiny : PageSectionHeight.Tall;
        }

        public async Task<PageSectionBackgroundStyle> DetermineBackgroundStyleAsync(int pageSectionId)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return PageSectionBackgroundStyle.Static;

            if (pageSection.PageSectionBody.Contains("background-static"))
                return PageSectionBackgroundStyle.Static;

            return pageSection.PageSectionBody.Contains("background-parallax") ? PageSectionBackgroundStyle.Parallax : PageSectionBackgroundStyle.Static;
        }

        public async Task<string> DetermineBackgroundTypeAsync(int pageSectionId)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return "background-picture";

            if (pageSection.PageSectionBody.Contains("background-colour"))
                return "background-colour";
            else
                return "background-picture";
        }

        public async Task<string> DetermineBackgroundColourAsync(int pageSectionId)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return "#ffffff";

            var pattern = new Regex(@"background-color: (?<existingColour>#[a-zA-Z0-9]{6,})");
            var match = pattern.Match(pageSection.PageSectionBody);
            var existingColour = match.Groups["existingColour"].Value;

            if (string.IsNullOrWhiteSpace(existingColour))
                return "#ffffff";

            return existingColour;
        }

        #region Backup Methods

        public async Task BackupAsync(int pageSectionId)
        {
            var pageSection = await GetAsync(pageSectionId);
            if (pageSection == null) return;

            var newBackup = new PageSectionBackup
            {
                PageSectionId = pageSection.PageSectionId,
                PageSectionBody = pageSection.PageSectionBody,
                DateAdded = DateTime.Now
            };

            _context.PageSectionBackups.Add(newBackup);

            await _context.SaveChangesAsync();
        }

        public async Task<string> RestoreBackupAsync(int pageSectionId, int backupId)
        {
            var pageSectionBackup = await _context.PageSectionBackups.SingleOrDefaultAsync(x => x.PageSectionBackupId == backupId);
            var pageSection = await GetAsync(pageSectionId);

            pageSection.PageSectionBody = pageSectionBackup.PageSectionBody;

            await _context.SaveChangesAsync();

            return pageSection.PageSectionBody;
        }

        public async Task DeleteBackupAsync(int backupId)
        {
            var pageSectionBackup = await _context.PageSectionBackups.SingleOrDefaultAsync(x => x.PageSectionBackupId == backupId);
            if (pageSectionBackup == null) return;

            _context.PageSectionBackups.Remove(pageSectionBackup);

            await _context.SaveChangesAsync();
        }

        #endregion Backup Methods
    }
}