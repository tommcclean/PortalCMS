using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public interface IPageService
    {
        IEnumerable<Page> Get();

        Page Get(int pageId);

        int Add(string pageName, string area, string controller, string action);

        void Edit(int pageId, string pageName, string area, string controller, string action);

        void Delete(int pageId);

        void Order(int pageId, string sectionList);
    }

    public class PageService : IPageService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public PageService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public IEnumerable<Page> Get()
        {
            var results = _context.Pages.OrderBy(x => x.PageName);

            return results;
        }

        public Page Get(int pageId)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);

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

            if (page == null)
                return;

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

            if (page == null)
                return;

            _context.Pages.Remove(page);

            _context.SaveChanges();
        }

        public void Order(int pageId, string sectionList)
        {
            var page = _context.Pages.SingleOrDefault(x => x.PageId == pageId);

            if (page == null)
                return;

            var sections = sectionList.Split(',');

            foreach (var sectionProperties in sections)
            {
                var properties = sectionProperties.Split('-');

                var orderId = properties[0];
                var sectionId = properties[1];

                var section = page.PageSections.SingleOrDefault(x => x.PageSectionId.ToString() == sectionId.ToString());

                if (section == null)
                    continue;

                section.PageSectionOrder = Convert.ToInt32(orderId);
            }

            _context.SaveChanges();
        }
    }
}