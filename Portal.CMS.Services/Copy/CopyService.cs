using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Copy
{
    public interface ICopyService
    {
        int Create(string copyName, string copyBody);

        void Edit(int copyId, string copyName, string copyBody);

        IEnumerable<CopyItem> Get();

        CopyItem Get(int copyId);

        CopyItem Get(string copyName);

        void Delete(int copyId);
    }

    public class CopyService : ICopyService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public CopyService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public int Create(string copyName, string copyBody)
        {
            var newCopy = new CopyItem
            {
                CopyName = copyName,
                CopyBody = copyBody,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.CopyItems.Add(newCopy);

            _context.SaveChanges();

            return newCopy.CopyId;
        }

        public void Edit(int copyId, string copyName, string copyBody)
        {
            var copy = _context.CopyItems.SingleOrDefault(x => x.CopyId == copyId);

            if (copy == null)
                return;

            copy.CopyName = copyName;
            copy.CopyBody = copyBody;
            copy.DateUpdated = DateTime.Now;

            _context.SaveChanges();
        }

        public IEnumerable<CopyItem> Get()
        {
            var copySections = _context.CopyItems.OrderBy(x => x.CopyName).ThenBy(x => x.CopyId);

            return copySections;
        }

        public CopyItem Get(int copyId)
        {
            var copyItem = _context.CopyItems.SingleOrDefault(x => x.CopyId == copyId);

            return copyItem;
        }

        public CopyItem Get(string copyName)
        {
            var copyItem = _context.CopyItems.FirstOrDefault(x => x.CopyName == copyName);

            return copyItem;
        }

        public void Delete(int copyId)
        {
            var copyItem = _context.CopyItems.SingleOrDefault(x => x.CopyId == copyId);

            if (copyItem != null)
                _context.CopyItems.Remove(copyItem);

            _context.SaveChanges();
        }
    }
}