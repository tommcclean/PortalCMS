using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Copy
{
    public class CopyViewModel
    {
        public IEnumerable<Portal.CMS.Entities.Entities.Copy.Copy> CopyList { get; set; }
    }
}