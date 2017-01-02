using Portal.CMS.Entities.Entities.Generic;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Shared
{
    public class PaginationViewModel
    {
        public string PaginationType { get; set; }

        public double PageCount { get; set; }

        public IEnumerable<Image> ImageList { get; set; }
    }
}