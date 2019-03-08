namespace PortalCMS.Web.DependencyResolution
{
    using Areas.Admin.Controllers;
    using Entities;
    using StructureMap;

    public static class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(
                c =>
                {
                    c.AddRegistry<DefaultRegistry>();
                    c.For<AnalyticManagerController>().AlwaysUnique();
                    c.For<PortalDbContext>().Use<PortalDbContext>().SelectConstructor(() => new PortalDbContext());
                }
            );
        }
    }
}