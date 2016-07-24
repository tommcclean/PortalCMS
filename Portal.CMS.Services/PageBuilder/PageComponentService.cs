using Portal.CMS.Entities;
using Portal.CMS.Services.Shared;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageComponentService
    {
        void Delete(int pageSectionId, string componentId);

        void EditImage(int pageSectionId, string elementId, int selectedImageId);

        void Anchor(int pageSectionId, string elementId, string elementText, string elementHref, string elementTarget);

        void Element(int pageSectionId, string elementId, string elementBody);
    }

    public class PageComponentService : IPageComponentService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PageComponentService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public void Element(int pageSectionId, string elementId, string elementBody)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementBody);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void Delete(int pageSectionId, string componentId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.DeleteElement(componentId);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void EditImage(int pageSectionId, string elementId, int selectedImageId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var image = _context.Images.SingleOrDefault(x => x.ImageId == selectedImageId);

            if (image == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(elementId, "style", string.Format("background-image: url('{0}');", image.ImagePath), true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void Anchor(int pageSectionId, string elementId, string elementText, string elementHref, string elementTarget)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementText);
            document.UpdateElementAttribute(elementId, "href", elementHref, true);
            document.UpdateElementAttribute(elementId, "target", elementTarget ?? "", true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }
    }
}