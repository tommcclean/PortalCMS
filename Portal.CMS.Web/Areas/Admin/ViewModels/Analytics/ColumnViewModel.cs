using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Analytics
{
    public class ColumnViewModel
    {
        public string ColumnName { get; set; }

        public List<int> ColumnValues { get; set; }
    }
}