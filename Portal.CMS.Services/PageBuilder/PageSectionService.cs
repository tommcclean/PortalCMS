using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using Portal.CMS.Services.Shared;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public class PageSectionService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public PageSectionService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public PageSection Get(int pageSectionId)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            return pageSection;
        }

        public int Add(int pageId, int pageSectionTypeId)
        {
            var sectionType = _context.PageSectionTypes.FirstOrDefault(x => x.PageSectionTypeId == pageSectionTypeId);

            var newPageSection = new PageSection()
            {
                PageId = pageId,
                PageSectionTypeId = pageSectionTypeId,
                PageSectionBody = sectionType.PageSectionTypeBody
            };

            _context.PageSections.Add(newPageSection);

            _context.SaveChanges();

            var document = new Document(newPageSection.PageSectionBody);

            newPageSection.PageSectionBody = document.ReplaceTokens(newPageSection.PageSectionBody, newPageSection.PageSectionId);

            _context.SaveChanges();

            return newPageSection.PageSectionId;
        }

        public void Element(int pageSectionId, string elementId, string elementValue, string elementColour)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

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
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var image = _context.Images.FirstOrDefault(x => x.ImageId == backgroundImageId);

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
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            _context.PageSections.Remove(pageSection);

            _context.SaveChanges();
        }

        public void Height(int pageSectionId, PageSectionHeight height)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateSectionHeight(string.Format("section-{0}", pageSectionId), height);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void BackgroundType(int pageSectionId, PageSectionBackgroundType backgroundType)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateBackgroundType(string.Format("section-{0}", pageSectionId), backgroundType);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public PageSectionHeight DetermineSectionHeight(int pageSectionId)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return PageSectionHeight.Tall;

            if (pageSection.PageSectionBody.Contains("height-tall"))
                return PageSectionHeight.Tall;
            else if (pageSection.PageSectionBody.Contains("height-medium"))
                return PageSectionHeight.Medium;
            if (pageSection.PageSectionBody.Contains("height-small"))
                return PageSectionHeight.Small;
            if (pageSection.PageSectionBody.Contains("height-tiny"))
                return PageSectionHeight.Tiny;
            else
                return PageSectionHeight.Tall;
        }

        public PageSectionBackgroundType DetermineBackgroundType(int pageSectionId)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return PageSectionBackgroundType.Static;

            if (pageSection.PageSectionBody.Contains("background-static"))
                return PageSectionBackgroundType.Static;
            else if (pageSection.PageSectionBody.Contains("background-parallax"))
                return PageSectionBackgroundType.Parallax;
            else
                return PageSectionBackgroundType.Static;
        }

        public void Markup(int pageSectionId, string htmlBody)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            pageSection.PageSectionBody = htmlBody;

            _context.SaveChanges();
        }
    }
}