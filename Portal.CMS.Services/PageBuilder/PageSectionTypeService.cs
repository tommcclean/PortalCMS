using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;
using System.Linq;
using Portal.CMS.Entities.Seed;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageSectionTypeService
    {
        IEnumerable<PageSectionType> Get();

        void Reset();
    }

    public class PageSectionTypeService : IPageSectionTypeService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PageSectionTypeService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public IEnumerable<PageSectionType> Get()
        {
            var results = _context.PageSectionTypes.OrderBy(x => x.PageSectionTypeId);

            return results;
        }

        public void Reset()
        {
            var sectionTypes = _context.PageSectionTypes;

            foreach (var sectionType in sectionTypes)
                _context.PageSectionTypes.Remove(sectionType);

            _context.SaveChanges();

            PageSectionTypeSeed.Seed(_context);

            _context.SaveChanges();
        }
    }
}