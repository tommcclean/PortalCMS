using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.PageBuilder.ViewModels.Section
{
    public class CloneViewModel
    {
        [DisplayName("Page")]
        public int PageId { get; set; }

        public int PageAssociationId { get; set; }

        internal List<Page> PageList { get; set; }

        public IEnumerable<SelectListItem> PageListOptions => PageList.Select(x => new SelectListItem
        {
            Value = x.PageId.ToString(),
            Text = x.PageName
        });
    }
}