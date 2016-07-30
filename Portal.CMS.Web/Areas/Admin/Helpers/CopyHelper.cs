using Portal.CMS.Services.Copy;
using Portal.CMS.Web.DependencyResolution;
using StructureMap;

namespace Portal.CMS.Web.Areas.Admin.Helpers
{
    public static class CopyHelper
    {
        public static string Get(string copyName)
        {
            IContainer container = IoC.Initialize();

            ICopyService copyService = container.GetInstance<CopyService>();

            var copyItem = copyService.Get(copyName);

            return copyItem.CopyBody;
        }
    }
}