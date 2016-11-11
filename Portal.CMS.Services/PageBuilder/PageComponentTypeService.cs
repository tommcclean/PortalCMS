using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using Portal.CMS.Services.Shared;
using System.Collections.Generic;
using System.Linq;
using Portal.CMS.Entities.Seed;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageComponentTypeService
    {
        IEnumerable<PageComponentType> Get();

        PageComponentType Get(int pageComponentTypeId);

        int Add(string componentTypeName, PageComponentTypeCategory componentTypeCategory, string componentTypeBody);

        void Reset();
    }

    public class PageComponentTypeService : IPageComponentTypeService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

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

        public int Add(string componentTypeName, PageComponentTypeCategory componentTypeCategory, string componentTypeBody)
        {
            var componentType = new PageComponentType
            {
                PageComponentTypeName = componentTypeName,
                PageComponentTypeCategory = componentTypeCategory,
                PageComponentBody = componentTypeBody
            };

            _context.PageComponentTypes.Add(componentType);

            _context.SaveChanges();

            return componentType.PageComponentTypeId;
        }

        public void Reset()
        {
            var componentTypeList = _context.PageComponentTypes;

            foreach (var componentType in componentTypeList)
                _context.PageComponentTypes.Remove(componentType);

            _context.SaveChanges();

            PageComponentTypeSeed.Seed(_context);

            _context.SaveChanges();
        }
    }
}