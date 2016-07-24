using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using Portal.CMS.Services.Shared;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageSectionService
    {
        PageSection Get(int pageSectionId);

        int Add(int pageId, int pageSectionTypeId);

        void Element(int pageSectionId, string elementId, string elementValue, string elementColour);

        void Background(int pageSectionId, int backgroundImageId);

        void Delete(int pageSectionId);

        void Height(int pageSectionId, PageSectionHeight height);

        void BackgroundType(int pageSectionId, PageSectionBackgroundType backgroundType);

        PageSectionHeight DetermineSectionHeight(int pageSectionId);

        PageSectionBackgroundType DetermineBackgroundType(int pageSectionId);

        void Markup(int pageSectionId, string htmlBody);
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

        public int Add(int pageId, int pageSectionTypeId)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);

            var sectionType = _context.PageSectionTypes.SingleOrDefault(x => x.PageSectionTypeId == pageSectionTypeId);

            var sectionPosition = 1;

            if (page.PageSections.Any())
                sectionPosition = (page.PageSections.Max(x => x.PageSectionId) + 1);

            var newPageSection = new PageSection
            {
                PageId = pageId,
                PageSectionTypeId = pageSectionTypeId,
                PageSectionBody = sectionType.PageSectionTypeBody,
                PageSectionOrder = sectionPosition
            };

            _context.PageSections.Add(newPageSection);

            _context.SaveChanges();

            var document = new Document(newPageSection.PageSectionBody);

            newPageSection.PageSectionBody = Document.ReplaceTokens(newPageSection.PageSectionBody, newPageSection.PageSectionId);

            _context.SaveChanges();

            return newPageSection.PageSectionId;
        }

        public void Element(int pageSectionId, string elementId, string elementValue, string elementColour)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementValue);

            if (!string.IsNullOrWhiteSpace(elementColour))
                document.UpdateElementAttribute(elementId, "style", string.Format("color: {0};", elementColour), true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
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

        public void Delete(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            _context.PageSections.Remove(pageSection);

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

        public void BackgroundType(int pageSectionId, PageSectionBackgroundType backgroundType)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateBackgroundType(string.Format("section-{0}", pageSectionId), backgroundType);

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

        public PageSectionBackgroundType DetermineBackgroundType(int pageSectionId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return PageSectionBackgroundType.Static;

            if (pageSection.PageSectionBody.Contains("background-static"))
                return PageSectionBackgroundType.Static;

            return pageSection.PageSectionBody.Contains("background-parallax") ? PageSectionBackgroundType.Parallax : PageSectionBackgroundType.Static;
        }

        public void Markup(int pageSectionId, string htmlBody)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            pageSection.PageSectionBody = htmlBody;

            _context.SaveChanges();
        }
    }
}