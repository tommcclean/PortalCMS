using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageAssociationService
    {
        Task<List<PagePartial>> GetAsync();

        Task<PageAssociation> GetAsync(int pageAssociationId);

        Task DeleteAsync(int pageAssociationId);

        Task EditRolesAsync(int pageAssociationId, List<string> roleList);

        Task EditOrderAsync(int pageId, string associationList);

        Task CloneAsync(int pageAssociationId, int pageId);
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

        public async Task<List<PagePartial>> GetAsync()
        {
            var existingPartialList = await _context.PagePartials.ToListAsync();

            var distinctPartialList = new List<PagePartial>();

            foreach (var partial in existingPartialList)
                if (!distinctPartialList.Any(x => x.RouteArea == partial.RouteArea && x.RouteController == partial.RouteController && x.RouteAction == partial.RouteAction))
                    distinctPartialList.Add(partial);

            return distinctPartialList;
        }

        public async Task<PageAssociation> GetAsync(int pageAssociationId)
        {
            var pageAssociation = await _context.PageAssociations.SingleOrDefaultAsync(pa => pa.PageAssociationId == pageAssociationId);

            return pageAssociation;
        }

        public async Task DeleteAsync(int pageAssociationId)
        {
            var pageAssociation = await _context.PageAssociations.SingleOrDefaultAsync(x => x.PageAssociationId == pageAssociationId);
            if (pageAssociation == null) return;

            if (pageAssociation.PageSection != null)
            {
                if (!await _context.PageAssociations.AnyAsync(x => x.PageSectionId == pageAssociation.PageSectionId))
                {
                    var pageSection = await _context.PageSections.SingleOrDefaultAsync(x => x.PageSectionId == pageAssociation.PageSectionId);

                    _context.PageSections.Remove(pageSection);
                }
            }
            else if (pageAssociation.PagePartial != null)
            {
                if (!_context.PageAssociations.Any(x => x.PagePartialId == pageAssociation.PagePartialId))
                {
                    var pagePartial = await _context.PagePartials.SingleOrDefaultAsync(x => x.PagePartialId == pageAssociation.PagePartialId);

                    _context.PagePartials.Remove(pagePartial);
                }
            }

            _context.PageAssociations.Remove(pageAssociation);

            await _context.SaveChangesAsync();
        }

        public async Task EditRolesAsync(int pageAssociationId, List<string> roleList)
        {
            var pageAssociation = await _context.PageAssociations.SingleOrDefaultAsync(pa => pa.PageAssociationId == pageAssociationId);
            if (pageAssociation == null) return;

            var roles = await _context.Roles.ToListAsync();

            foreach (var role in pageAssociation.PageAssociationRoles.ToList())
                _context.PageAssociationRoles.Remove(role);

            foreach (var roleName in roleList)
            {
                var currentRole = roles.FirstOrDefault(x => x.RoleName == roleName);
                if (currentRole == null) continue;

                _context.PageAssociationRoles.Add(new PageAssociationRole { PageAssociationId = pageAssociationId, RoleId = currentRole.RoleId });
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditOrderAsync(int pageId, string associationList)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId == pageId);
            if (page == null) return;

            var associations = associationList.Split(',');

            foreach (var associationProperties in associations)
            {
                var properties = associationProperties.Split('-');

                var orderId = properties[0];
                var associationId = properties[1];

                var pageAssociation = page.PageAssociations.SingleOrDefault(x => x.PageAssociationId.ToString() == associationId.ToString());

                if (pageAssociation == null)
                    continue;

                pageAssociation.PageAssociationOrder = Convert.ToInt32(orderId);
            }

            await _context.SaveChangesAsync();
        }

        public async Task CloneAsync(int pageAssociationId, int pageId)
        {
            var pageAssociation = await _context.PageAssociations.FirstOrDefaultAsync(pa => pa.PageAssociationId == pageAssociationId);
            if (pageAssociation == null) return;

            var page = await _context.Pages.FirstOrDefaultAsync(p => p.PageId == pageId);
            if (page == null) return;

            if (pageAssociation.PageSection != null)
            {
                var clonePageAssociation = new PageAssociation
                {
                    PageSectionId = pageAssociation.PageSectionId,
                    PageId = page.PageId,
                    PageAssociationRoles = pageAssociation.PageAssociationRoles,
                };

                if (page.PageAssociations.Any())
                    clonePageAssociation.PageAssociationOrder = page.PageAssociations.Max(pa => pa.PageAssociationOrder + 1);
                else
                    clonePageAssociation.PageAssociationOrder = 1;

                _context.PageAssociations.Add(clonePageAssociation);

                await _context.SaveChangesAsync();
            }
        }
    }
}