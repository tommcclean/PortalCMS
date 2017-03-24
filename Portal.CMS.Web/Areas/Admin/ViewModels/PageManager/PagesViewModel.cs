using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.PageManager
{
    public class PagesViewModel
    {
        public IEnumerable<Page> PageList { get; set; }

        public List<string> PageAreas { get; set; }
    }
}