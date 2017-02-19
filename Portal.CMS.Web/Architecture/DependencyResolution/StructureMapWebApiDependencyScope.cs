using StructureMap;
using System.Web.Http.Dependencies;

namespace Portal.CMS.Web.DependencyResolution
{
    /// <summary>
    /// The structure map web api dependency scope.
    /// </summary>
    internal class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container)
        {
        }
    }
}