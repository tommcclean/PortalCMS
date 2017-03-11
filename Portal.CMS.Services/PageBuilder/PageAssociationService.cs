using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageAssociationService
    {
        PageAssociation Get(int pageAssociationId);

        void Delete(int pageAssociationId);

        void EditRoles(int pageAssociationId, List<string> roleList);

        void EditOrder(int pageId, string associationList);
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
            var pageAssociation = _context.PageAssociations.SingleOrDefault(pa => pa.PageAssociationId == pageAssociationId);

            return pageAssociation;
        }

        public void Delete(int pageAssociationId)
        {
            var pageAssociation = _context.PageAssociations.SingleOrDefault(x => x.PageAssociationId == pageAssociationId);
            if (pageAssociation == null) return;

            if (pageAssociation.PageSection != null)
            {
                if (!_context.PageAssociations.Any(x => x.PageSectionId == pageAssociation.PageSectionId))
                {
                    var pageSection = _context.PageSections.SingleOrDefault(x => x.PageSectionId == pageAssociation.PageSectionId);

                    _context.PageSections.Remove(pageSection);
                }
            }
            else if (pageAssociation.PagePartial != null)
            {
                if (!_context.PageAssociations.Any(x => x.PagePartialId == pageAssociation.PagePartialId))
                {
                    var pagePartial = _context.PagePartials.SingleOrDefault(x => x.PagePartialId == pageAssociation.PagePartialId);

                    _context.PagePartials.Remove(pagePartial);
                }
            }

            _context.PageAssociations.Remove(pageAssociation);

            _context.SaveChanges();
        }

        public void EditRoles(int pageAssociationId, List<string> roleList)
        {
            var pageAssociation = _context.PageAssociations.SingleOrDefault(pa => pa.PageAssociationId == pageAssociationId);
            if (pageAssociation == null) return;

            var roles = _context.Roles.ToList();

            foreach (var role in pageAssociation.PageAssociationRoles.ToList())
                _context.PageAssociationRoles.Remove(role);

            foreach (var roleName in roleList)
            {
                var currentRole = roles.FirstOrDefault(x => x.RoleName == roleName);
                if (currentRole == null) continue;

                _context.PageAssociationRoles.Add(new PageAssociationRole { PageAssociationId = pageAssociationId, RoleId = currentRole.RoleId });
            }

            _context.SaveChanges();
        }

        public void EditOrder(int pageId, string associationList)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);
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

            _context.SaveChanges();
        }
    }
}