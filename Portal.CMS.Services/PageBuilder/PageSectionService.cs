using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using Portal.CMS.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageSectionService
    {
        PageSection Get(int pageSectionId);

        PageAssociation Add(int pageId, int pageSectionTypeId, string componentStamp);

        void Background(int pageSectionId, int backgroundImageId);

        void Background(int pageSectionId, string backgroundColour);

        void Delete(int pageSectionId);

        void Height(int pageSectionId, PageSectionHeight height);

        void SetBackgroundStyle(int pageSectionId, PageSectionBackgroundStyle backgroundType);

        void SetBackgroundType(int pageSectionId, bool isPicture);

        PageSectionHeight DetermineSectionHeight(int pageSectionId);

        PageSectionBackgroundStyle DetermineBackgroundStyle(int pageSectionId);

        string DetermineBackgroundType(int pageSectionId);

        string DetermineBackgroundColour(int pageSectionId);

        void Markup(int pageSectionId, string htmlBody);

        void Roles(int pageSectionId, List<string> roleList);

        void Backup(int pageSectionId);

        string RestoreBackup(int pageSectionId, int backupId);

        void DeleteBackup(int backupId);

        void SetAnimation(int pageSectionId, string elementId, string animation);
    }

    public class PageSectionService : IPageSectionService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PageSectionService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public PageSection Get(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            return pageSection;
        }

        public PageAssociation Add(int pageId, int pageSectionTypeId, string componentStamp)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);

            var sectionType = _context.PageSectionTypes.SingleOrDefault(x => x.PageSectionTypeId == pageSectionTypeId);

            var sectionPosition = 1;

            if (page.PageAssociations.Any())
                sectionPosition = (page.PageAssociations.Max(x => x.PageAssociationOrder) + 1);

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

            _context.SaveChanges();

            var document = new Document(newPageAssociation.PageSection.PageSectionBody);

            newPageAssociation.PageSection.PageSectionBody = Document.ReplaceTokens(newPageAssociation.PageSection.PageSectionBody, newPageAssociation.PageSection.PageSectionId, componentStamp);

            _context.SaveChanges();

            return newPageAssociation;
        }

        public void Background(int pageSectionId, int backgroundImageId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var image = _context.Images.SingleOrDefault(x => x.ImageId == backgroundImageId);

            if (image == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", string.Format("background-image: url('{0}');", image.ImagePath), true);

            if (image.ImageCategory == Entities.Entities.Generic.ImageCategory.Texture)
                document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", "background-size: initial;", false);
            else
                document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", "background-size: cover;", false);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void Background(int pageSectionId, string backgroundColour)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(string.Format("section-{0}", pageSectionId), "style", string.Format("background-color: {0};", backgroundColour), true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void Delete(int pageAssociationId)
        {
            var pageAssociation = _context.PageAssociations.SingleOrDefault(x => x.PageAssociationId == pageAssociationId);
            if (pageAssociation == null) return;

            var pageSectionId = pageAssociation.PageSectionId;

            _context.PageAssociations.Remove(pageAssociation);

            if (!_context.PageAssociations.Any(x => x.PageSectionId == pageAssociation.PageSectionId))
            {
                var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

                if (pageSection != null)
                    _context.PageSections.Remove(pageSection);
            }

            _context.SaveChanges();
        }

        public void Height(int pageSectionId, PageSectionHeight height)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateSectionHeight(string.Format("section-{0}", pageSectionId), height);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void SetBackgroundStyle(int pageSectionId, PageSectionBackgroundStyle backgroundType)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateBackgroundStyle(string.Format("section-{0}", pageSectionId), backgroundType);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void SetBackgroundType(int pageSectionId, bool isPicture)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateBackgroundType(string.Format("section-{0}", pageSectionId), isPicture);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public PageSectionHeight DetermineSectionHeight(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

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

        public PageSectionBackgroundStyle DetermineBackgroundStyle(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return PageSectionBackgroundStyle.Static;

            if (pageSection.PageSectionBody.Contains("background-static"))
                return PageSectionBackgroundStyle.Static;

            return pageSection.PageSectionBody.Contains("background-parallax") ? PageSectionBackgroundStyle.Parallax : PageSectionBackgroundStyle.Static;
        }

        public string DetermineBackgroundType(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return "background-picture";

            if (pageSection.PageSectionBody.Contains("background-colour"))
                return "background-colour";
            else
                return "background-picture";
        }

        public string DetermineBackgroundColour(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return "#ffffff";

            var pattern = new Regex(@"(?<existingColour>#\d{6})");
            var match = pattern.Match(pageSection.PageSectionBody);
            var existingColour = match.Groups["existingColour"].Value;

            if (string.IsNullOrWhiteSpace(existingColour))
                return "#ffffff";

            return existingColour;
        }

        public void Markup(int pageSectionId, string htmlBody)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            pageSection.PageSectionBody = htmlBody;

            _context.SaveChanges();
        }

        public void Roles(int pageSectionId, List<string> roleList)
        {
            var pageSection = Get(pageSectionId);

            if (pageSection == null)
                return;

            var roles = _context.Roles.ToList();

            if (pageSection != null)
                foreach (var role in pageSection.PageSectionRoles.ToList())
                    _context.PageSectionRoles.Remove(role);

            foreach (var roleName in roleList)
            {
                var currentRole = roles.FirstOrDefault(x => x.RoleName == roleName);

                if (currentRole == null)
                    continue;

                _context.PageSectionRoles.Add(new PageSectionRole { PageSectionId = pageSectionId, RoleId = currentRole.RoleId });
            }

            _context.SaveChanges();
        }

        public void Backup(int pageSectionId)
        {
            var pageSection = Get(pageSectionId);

            if (pageSection == null) return;

            var newBackup = new PageSectionBackup
            {
                PageSectionId = pageSection.PageSectionId,
                PageSectionBody = pageSection.PageSectionBody,
                DateAdded = DateTime.Now
            };

            _context.PageSectionBackups.Add(newBackup);

            _context.SaveChanges();
        }

        public string RestoreBackup(int pageSectionId, int backupId)
        {
            var pageSectionBackup = _context.PageSectionBackups.FirstOrDefault(x => x.PageSectionBackupId == backupId);

            var pageSection = Get(pageSectionId);

            pageSection.PageSectionBody = pageSectionBackup.PageSectionBody;

            _context.SaveChanges();

            return pageSection.PageSectionBody;
        }

        public void DeleteBackup(int backupId)
        {
            var pageSectionBackup = _context.PageSectionBackups.FirstOrDefault(x => x.PageSectionBackupId == backupId);

            if (pageSectionBackup == null) return;

            _context.PageSectionBackups.Remove(pageSectionBackup);

            _context.SaveChanges();
        }

        public void SetAnimation(int pageSectionId, string elementId, string animation)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateAnimation(elementId, animation);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }
    }
}