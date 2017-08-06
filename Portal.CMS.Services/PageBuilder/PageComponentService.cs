using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Shared;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageComponentService
    {
        Task<List<PageComponentType>> GetComponentTypesAsync();

        Task AddAsync(int pageSectionId, string containerElementId, string elementBody);

        Task EditImageAsync(int pageSectionId, string elementType, string elementId, string imagePath);

        Task EditAnchorAsync(int pageSectionId, string elementId, string elementText, string elementHref, string elementTarget);

        Task EditElementAsync(int pageSectionId, string elementId, string elementBody);

        Task EditSourceAsync(int pageSectionId, string elementId, string newSourcePath);

        Task CloneElementAsync(int pageSectionId, string elementId, string componentStamp);

        Task DeleteAsync(int pageSectionId, string componentId);
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

        public async Task<List<PageComponentType>> GetComponentTypesAsync()
        {
            var results = await _context.PageComponentTypes.OrderBy(x => x.PageComponentTypeId).ToListAsync();

            return results;
        }

        public async Task AddAsync(int pageSectionId, string containerElementId, string elementBody)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.AddElement(containerElementId, elementBody);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditElementAsync(int pageSectionId, string elementId, string elementBody)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementBody);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditImageAsync(int pageSectionId, string elementType, string elementId, string imagePath)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            if (elementType.Equals("div", System.StringComparison.OrdinalIgnoreCase))
                document.UpdateElementAttribute(elementId, "style", string.Format("background-image: url('{0}');", imagePath), true);
            else
                document.UpdateElementAttribute(elementId, "src", imagePath, true);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditAnchorAsync(int pageSectionId, string elementId, string elementText, string elementHref, string elementTarget)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementContent(elementId, elementText);
            document.UpdateElementAttribute(elementId, "href", elementHref, true);
            document.UpdateElementAttribute(elementId, "target", elementTarget ?? "", true);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task EditSourceAsync(int pageSectionId, string elementId, string newSourcePath)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.UpdateElementAttribute(elementId, "src", newSourcePath, true);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task CloneElementAsync(int pageSectionId, string elementId, string componentStamp)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.CloneElement(pageSectionId, elementId, componentStamp);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int pageSectionId, string componentId)
        {
            var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageSectionId);
            if (pageSection == null) return;

            var document = new Document(pageSection.PageSectionBody);

            document.DeleteElement(componentId);

            pageSection.PageSectionBody = document.OuterHtml;

            await _context.SaveChangesAsync();
        }
    }
}