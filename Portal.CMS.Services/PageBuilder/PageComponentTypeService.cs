using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using Portal.CMS.Services.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageComponentTypeService
    {
        IEnumerable<PageComponentType> Get();

        PageComponentType Get(int pageComponentTypeId);

        void Add(int pageSectionId, string containerElementId, string elementBody);
    }

    public class PageComponentTypeService : IPageComponentTypeService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public PageComponentTypeService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public IEnumerable<PageComponentType> Get()
        {
            var results = _context.PageComponentTypes.OrderBy(x => x.PageComponentTypeId);

            return results;
        }

        public PageComponentType Get(int pageComponentTypeId)
        {
            var result = _context.PageComponentTypes.SingleOrDefault(x => x.PageComponentTypeId == pageComponentTypeId);

            return result;
        }

        public void Add(int pageSectionId, string containerElementId, string elementBody)
        {
            var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.AddElement(containerElementId, elementBody);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }
    }
}