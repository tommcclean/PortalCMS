using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.CopyManager
{
    public class CopyViewModel
    {
        public IEnumerable<CopyItem> CopyList { get; set; }
    }
}