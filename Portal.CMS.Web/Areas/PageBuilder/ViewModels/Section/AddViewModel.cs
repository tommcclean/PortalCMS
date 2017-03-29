using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.PageBuilder.ViewModels.Section
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