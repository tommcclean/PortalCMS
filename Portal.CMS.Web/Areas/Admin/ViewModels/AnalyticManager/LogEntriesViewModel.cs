using LogBook.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.AnalyticManager
{
    public class LogEntriesViewModel
    {
        public IEnumerable<LogEntry> LogEntries { get; set; }
    }
}