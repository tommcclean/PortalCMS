using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageAssociationService
    {
        PageAssociation Get(int pageAssociationId);
    }

    public class PageAssociationService : IPageAssociationService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PageAssociationService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public PageAssociation Get(int pageAssociationId)
        {
            var pageAssociation = _context.PageAssociations.FirstOrDefault(pa => pa.PageAssociationId == pageAssociationId);

            return pageAssociation;
        }
    }
}