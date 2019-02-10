using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.CopyManager
{
    public class CopyViewModel
    {
        public IEnumerable<CopyItem> CopyList { get; set; }
    }
}