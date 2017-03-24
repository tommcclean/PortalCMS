using StructureMap;
using System.Web.Http.Dependencies;

namespace Portal.CMS.Web.DependencyResolution
{
    /// <summary>
    /// The structure map dependency resolver.
    /// </summary>
    internal class StructureMapWebApiDependencyResolver : StructureMapWebApiDependencyScope, IDependencyResolver
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapWebApiDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapWebApiDependencyResolver(IContainer container)
            : base(container)
        {
        }

        #endregion Constructors and Destructors

        #region Public Methods and Operators

        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <returns>The System.Web.Http.Dependencies.IDependencyScope.</returns>
        public IDependencyScope BeginScope()
        {
            var child = Container.GetNestedContainer();
            return new StructureMapWebApiDependencyResolver(child);
        }

        #endregion Public Methods and Operators
    }
}