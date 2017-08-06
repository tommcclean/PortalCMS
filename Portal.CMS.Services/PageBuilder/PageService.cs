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
        Task<List<Page>> GetAsync();

        Task<Page> ViewAsync(int? userId, int pageId);

        Task<Page> GetAsync(int pageId);

        Task<int> AddAsync(string pageName, string area, string controller, string action);

        Task EditAsync(int pageId, string pageName, string area, string controller, string action);

        Task DeleteAsync(int pageId);

        Task RolesAsync(int pageId, List<string> roleList);
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

        public async Task<List<Page>> GetAsync()
        {
            var results = await _context.Pages.OrderBy(x => x.PageName).ToListAsync();

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

        public async Task<Page> GetAsync(int pageId)
        {
            var page = await _context.Pages.Include(x => x.PageAssociations).SingleOrDefaultAsync(x => x.PageId == pageId);

            return page;
        }

        public async Task<int> AddAsync(string pageName, string area, string controller, string action)
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

            await _context.SaveChangesAsync();

            return newPage.PageId;
        }

        public async Task EditAsync(int pageId, string pageName, string area, string controller, string action)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId == pageId);
            if (page == null) return;

            page.PageName = pageName;
            page.PageArea = area;
            page.PageController = controller;
            page.PageAction = action;
            page.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int pageId)
        {
            var page = await _context.Pages.SingleOrDefaultAsync(x => x.PageId == pageId);
            if (page == null) return;

            _context.Pages.Remove(page);

            await _context.SaveChangesAsync();
        }

        public async Task RolesAsync(int pageId, List<string> roleList)
        {
            var page = await GetAsync(pageId);
            if (page == null) return;

            var roles = await _context.Roles.ToListAsync();

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

            await _context.SaveChangesAsync();
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