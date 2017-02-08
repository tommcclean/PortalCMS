using Portal.CMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Copy
{
    public interface ICopyService
    {
        int Create(string copyName, string copyBody);

        void Edit(int copyId, string copyName, string copyBody);

        IEnumerable<Portal.CMS.Entities.Entities.Copy.Copy> Get();

        Portal.CMS.Entities.Entities.Copy.Copy Get(int copyId);

        Entities.Entities.Copy.Copy Get(string copyName);

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
            var newCopy = new Portal.CMS.Entities.Entities.Copy.Copy
            {
                CopyName = copyName,
                CopyBody = copyBody,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.CopySections.Add(newCopy);

            _context.SaveChanges();

            return newCopy.CopyId;
        }

        public void Edit(int copyId, string copyName, string copyBody)
        {
            var copy = _context.CopySections.SingleOrDefault(x => x.CopyId == copyId);

            if (copy == null)
                return;

            copy.CopyName = copyName;
            copy.CopyBody = copyBody;
            copy.DateUpdated = DateTime.Now;

            _context.SaveChanges();
        }

        public IEnumerable<Portal.CMS.Entities.Entities.Copy.Copy> Get()
        {
            var copySections = _context.CopySections.OrderBy(x => x.CopyName).ThenBy(x => x.CopyId);

            return copySections;
        }

        public Portal.CMS.Entities.Entities.Copy.Copy Get(int copyId)
        {
            var copyItem = _context.CopySections.SingleOrDefault(x => x.CopyId == copyId);

            return copyItem;
        }

        public Entities.Entities.Copy.Copy Get(string copyName)
        {
            var copyItem = _context.CopySections.FirstOrDefault(x => x.CopyName == copyName);

            return copyItem;
        }

        public void Delete(int copyId)
        {
            var copyItem = _context.CopySections.SingleOrDefault(x => x.CopyId == copyId);

            if (copyItem != null)
                _context.CopySections.Remove(copyItem);

            _context.SaveChanges();
        }
    }
}