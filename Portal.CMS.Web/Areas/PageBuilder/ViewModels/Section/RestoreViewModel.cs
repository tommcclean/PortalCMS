using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.PageBuilder.ViewModels.Section
{
    public class RestoreViewModel
    {
        public int PageSectionId { get; set; }

        public List<PageSectionBackup> PageSectionBackup { get; set; }
    }
}