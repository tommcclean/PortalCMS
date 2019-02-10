using PortalCMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalCMS.Web.ViewModels.Shared
{
    public class PaginationViewModel
    {
        public string PaginationType { get; set; }

        public string TargetInputField { get; set; }

        public IEnumerable<Image> ImageList { get; set; }

        public double PageCount
        {
            get
            {
                return Math.Ceiling(Convert.ToDouble(ImageList.Count()) / 8);
            }
        }
    }
}