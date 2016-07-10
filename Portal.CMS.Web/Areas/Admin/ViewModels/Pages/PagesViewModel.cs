using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Pages
{
    public class PagesViewModel
    {
        public IEnumerable<Page> PageList { get; set; }
    }
}