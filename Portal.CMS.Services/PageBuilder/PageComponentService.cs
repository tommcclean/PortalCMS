using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageComponentService
    {
        List<PageComponentType> GetComponentTypes();

        void Add(int pageSectionId, string containerElementId, string elementBody);

        void EditImage(int pageSectionId, string elementType, string elementId, int selectedImageId);

        void EditAnchor(int pageSectionId, string elementId, string elementText, string elementHref, string elementTarget);

        void EditElement(int pageSectionId, string elementId, string elementBody);

        void EditSource(int pageSectionId, string elementId, string newSourcePath);

        void Delete(int pageSectionId, string componentId);
    }

    public class PageComponentService : IPageComponentService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public PageComponentService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public List<PageComponentType> GetComponentTypes()
        {
            var results = _context.PageComponentTypes.OrderBy(x => x.PageComponentTypeId).ToList();

            return results;
        }

        public void Add(int pageSectionId, string containerElementId, string elementBody)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.AddElement(containerElementId, elementBody);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void EditElement(int pageSectionId, string elementId, string elementBody)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementBody);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void EditImage(int pageSectionId, string elementType, string elementId, int selectedImageId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var image = _context.Images.SingleOrDefault(x => x.ImageId == selectedImageId);
            if (image == null) return;

            var document = new Document(pageSection.PageSectionBody);

            if (elementType.Equals("div", System.StringComparison.OrdinalIgnoreCase))
                document.UpdateElementAttribute(elementId, "style", string.Format("background-image: url('{0}');", image.ImagePath), true);
            else
                document.UpdateElementAttribute(elementId, "src", image.ImagePath, true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void EditAnchor(int pageSectionId, string elementId, string elementText, string elementHref, string elementTarget)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementText);
            document.UpdateElementAttribute(elementId, "href", elementHref, true);
            document.UpdateElementAttribute(elementId, "target", elementTarget ?? "", true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void EditSource(int pageSectionId, string elementId, string newSourcePath)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(elementId, "src", newSourcePath, true);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }

        public void Delete(int pageSectionId, string componentId)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.DeleteElement(componentId);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }
    }
}