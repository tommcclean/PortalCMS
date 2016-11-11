using System.Collections.Generic;
using Portal.CMS.Entities.Entities.PageBuilder;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.DevelopmentManager
{
    public class SectionTypeLibraryViewModel
    {
        public IEnumerable<PageSectionType> PageSectionTypes { get; set; }
    }
}