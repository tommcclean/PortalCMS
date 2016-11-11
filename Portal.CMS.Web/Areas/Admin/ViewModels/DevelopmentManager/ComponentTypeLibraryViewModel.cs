using System.Collections.Generic;
using Portal.CMS.Entities.Entities.PageBuilder;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.DevelopmentManager
{
    public class ComponentTypeLibraryViewModel
    {
        public IEnumerable<PageComponentType> PageComponentTypes { get; set; }
    }
}