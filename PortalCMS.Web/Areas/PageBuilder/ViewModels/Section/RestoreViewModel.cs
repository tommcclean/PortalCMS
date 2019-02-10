using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.PageBuilder.ViewModels.Section
{
    public class RestoreViewModel
    {
        public int PageSectionId { get; set; }

        public List<PageSectionBackup> PageSectionBackup { get; set; }
    }
}