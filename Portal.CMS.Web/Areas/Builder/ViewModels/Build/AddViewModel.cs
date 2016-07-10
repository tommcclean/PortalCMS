using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Build
{
    public class AddViewModel
    {
        public int PageId { get; set; }

        public int PageSectionTypeId { get; set; }

        public IEnumerable<PageSectionType> SectionTypeList { get; set; }
    }
}