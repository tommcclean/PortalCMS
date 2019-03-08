using PortalCMS.Entities.Entities;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.PageBuilder.ViewModels.Component
{
    public class AddViewModel
    {
        public string ContainerElementId { get; set; }

        public IEnumerable<PageComponentType> PageComponentTypeList { get; set; }
    }
}