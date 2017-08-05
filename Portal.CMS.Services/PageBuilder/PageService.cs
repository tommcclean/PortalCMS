using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageService
    {
        IEnumerable<Page> Get();

        Task<Page> ViewAsync(int? userId, int pageId);

        Page Get(int pageId);

        int Add(string pageName, string area, string controller, string action);

        void Edit(int pageId, string pageName, string area, string controller, string action);

        void Delete(int pageId);

        void Roles(int pageId, List<string> roleList);
    }

    public class PageService : IPageService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;
        private readonly IRoleService _roleService;

        public PageService(PortalEntityModel context, IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        #endregion Dependencies

        public IEnumerable<Page> Get()
        {
            var results = _context.Pages.OrderBy(x => x.PageName).ToList();

            return results;
        }

        public async Task<Page> ViewAsync(int? userId, int pageId)
        {
            var page = await _context.Pages.Include(x => x.PageAssociations).SingleOrDefaultAsync(x => x.PageId == pageId);

            if (!page.PageRoles.Any())
                return await FilterSectionListAsync(page, userId);

            var userRoles = await _roleService.GetAsync(userId);

            var hasAccess = _roleService.Validate(page.PageRoles.Select(x => x.Role), userRoles);

            if (hasAccess)
                return await FilterSectionListAsync(page, userId);

            return null;
        }

        public Page Get(int pageId)
        {
            var page = _context.Pages
                .Include(x => x.PageAssociations)
                .SingleOrDefault(x => x.PageId == pageId);

            return page;
        }

        public int Add(string pageName, string area, string controller, string action)
        {
            var newPage = new Page
            {
                PageName = pageName,
                PageArea = area,
                PageController = controller,
                PageAction = action,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.Pages.Add(newPage);

            _context.SaveChanges();

            return newPage.PageId;
        }

        public void Edit(int pageId, string pageName, string area, string controller, string action)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);
            if (page == null) return;

            page.PageName = pageName;
            page.PageArea = area;
            page.PageController = controller;
            page.PageAction = action;
            page.DateUpdated = DateTime.Now;

            _context.SaveChanges();
        }

        public void Delete(int pageId)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);
            if (page == null) return;

            _context.Pages.Remove(page);

            _context.SaveChanges();
        }

        public void Roles(int pageId, List<string> roleList)
        {
            var page = Get(pageId);
            if (page == null) return;

            var roles = _context.Roles.ToList();

            if (page.PageRoles != null)
                foreach (var role in page.PageRoles.ToList())
                    _context.PageRoles.Remove(role);

            foreach (var roleName in roleList)
            {
                var currentRole = roles.FirstOrDefault(x => x.RoleName == roleName);

                if (currentRole == null)
                    continue;

                _context.PageRoles.Add(new PageRole { PageId = pageId, RoleId = currentRole.RoleId });
            }

            _context.SaveChanges();
        }

        #region Private Methods

        private async Task<Page> FilterSectionListAsync(Page page, int? userId)
        {
            for (int loop = 0; loop < page.PageAssociations.Count(); loop += 1)
            {
                var pageAssociation = page.PageAssociations.ToList()[loop];

                if (pageAssociation != null)
                {
                    var userRoles = await _roleService.GetAsync(userId);

                    var hasAccess = _roleService.Validate(pageAssociation.PageAssociationRoles.Select(x => x.Role), userRoles);

                    if (!hasAccess)
                    {
                        page.PageAssociations.Remove(pageAssociation);

                        loop = loop - 1;
                    }
                }
            }

            return page;
        }

        #endregion Private Methods
    }
}