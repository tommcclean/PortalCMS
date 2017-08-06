using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Copy
{
    public interface ICopyService
    {
        Task<int> CreateAsync(string copyName, string copyBody);

        Task EditAsync(int copyId, string copyName, string copyBody);

        Task<IEnumerable<CopyItem>> GetAsync();

        Task<CopyItem> GetAsync(int copyId);

        Task<CopyItem> GetAsync(string copyName);

        Task DeleteAsync(int copyId);
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

        public async Task<int> CreateAsync(string copyName, string copyBody)
        {
            var newCopy = new CopyItem
            {
                CopyName = copyName,
                CopyBody = copyBody,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.CopyItems.Add(newCopy);

            await _context.SaveChangesAsync();

            return newCopy.CopyId;
        }

        public async Task EditAsync(int copyId, string copyName, string copyBody)
        {
            var copy = await _context.CopyItems.SingleOrDefaultAsync(x => x.CopyId == copyId);

            if (copy == null)
                return;

            copy.CopyName = copyName;
            copy.CopyBody = copyBody;
            copy.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CopyItem>> GetAsync()
        {
            var copySections = await _context.CopyItems.OrderBy(x => x.CopyName).ThenBy(x => x.CopyId).ToListAsync();

            return copySections;
        }

        public async Task<CopyItem> GetAsync(int copyId)
        {
            var copyItem = await _context.CopyItems.SingleOrDefaultAsync(x => x.CopyId == copyId);

            return copyItem;
        }

        public async Task<CopyItem> GetAsync(string copyName)
        {
            var copyItem = await _context.CopyItems.FirstOrDefaultAsync(x => x.CopyName == copyName);

            return copyItem;
        }

        public async Task DeleteAsync(int copyId)
        {
            var copyItem = await _context.CopyItems.SingleOrDefaultAsync(x => x.CopyId == copyId);

            if (copyItem != null)
                _context.CopyItems.Remove(copyItem);

            await _context.SaveChangesAsync();
        }
    }
}