using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.PageBuilder.ViewModels.Section
{
    public class AddViewModel
    {
        public int PageId { get; set; }

        #region Enumerable Properties

        public IEnumerable<PageSectionType> SectionTypeList { get; set; }

        public IEnumerable<PagePartial> PartialList { get; set; }

        #endregion Enumerable Properties
    }
}