using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageSectionTypeService
    {
        IEnumerable<PageSectionType> Get();
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
    }
}