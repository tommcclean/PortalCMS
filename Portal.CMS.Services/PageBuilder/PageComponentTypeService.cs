using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using Portal.CMS.Services.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public class PageComponentTypeService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public PageComponentTypeService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public IEnumerable<PageComponentType> Get()
        {
            var results = _context.PageComponentTypes.OrderBy(x => x.PageComponentTypeName).ThenBy(x => x.PageComponentTypeId);

            return results;
        }

        public PageComponentType Get(int pageComponentTypeId)
        {
            var result = _context.PageComponentTypes.FirstOrDefault(x => x.PageComponentTypeId == pageComponentTypeId);

            return result;
        }

        public void Add(int pageSectionId, string containerElementId, int componentTypeId)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var pageComponentType = Get(componentTypeId);

            if (pageComponentType == null)
                return;

            var document = new Document(pageSection.PageSectionBody);

            document.AddElement(pageSection.PageSectionId, containerElementId, pageComponentType.PageComponentBody);

            pageSection.PageSectionBody = document.OuterHtml;

            _context.SaveChanges();
        }
    }
}