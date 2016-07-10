using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Component
{
    public class AddViewModel
    {
        public int PageSectionId { get; set; }

        public int PageComponentTypeId { get; set; }

        public string ContainerElementId { get; set; }

        public IEnumerable<PageComponentType> PageComponentTypeList { get; set; }
    }
}