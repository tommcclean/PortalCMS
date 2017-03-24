namespace Portal.CMS.Web.DependencyResolution
{
    using StructureMap;
    using System.Web.Http.Dependencies;

    /// <summary>
    /// The structure map dependency resolver.
    /// </summary>
    internal class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapDependencyResolver(IContainer container)
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
            return new StructureMapDependencyResolver(child);
        }

        #endregion Public Methods and Operators
    }
}