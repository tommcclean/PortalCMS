using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Generic
{
    public interface IFontService
    {
        Font Get(int fontId);

        List<Font> Get();
    }

    public class FontService : IFontService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public FontService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public Font Get(int fontId)
        {
            var results = _context.Fonts.SingleOrDefault(x => x.FontId == fontId);

            return results;
        }

        public List<Font> Get()
        {
            var results = _context.Fonts.OrderByDescending(x => x.FontId).ToList();

            return results;
        }
    }
}
