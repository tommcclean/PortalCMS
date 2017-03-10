using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Section
{
    public class AddViewModel
    {
        public int PageId { get; set; }

        public int PageSectionTypeId { get; set; }

        #region Enumerable Properties

        public IEnumerable<PageSectionType> SectionTypeList { get; set; }

        #endregion Enumerable Properties
    }
}