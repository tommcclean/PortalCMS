using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPagePartialService
    {
        Task AddAsync(int pageId, string area, string controller, string action);
    }

    public class PagePartialService : IPagePartialService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PagePartialService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task AddAsync(int pageId, string area, string controller, string action)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId == pageId);
            if (page == null) return;

            var orderPosition = 0;

            if (page.PageAssociations.Any())
                orderPosition = page.PageAssociations.Max(x => x.PageAssociationOrder + 1);

            var pageAssociation = new PageAssociation
            {
                PageId = page.PageId,
                PageAssociationOrder = orderPosition,
                PagePartial = new PagePartial
                {
                    RouteArea = area,
                    RouteController = controller,
                    RouteAction = action
                }
            };

            _context.PageAssociations.Add(pageAssociation);

            await _context.SaveChangesAsync();
        }
    }
}