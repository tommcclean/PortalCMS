namespace Portal.CMS.Web.DependencyResolution
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
                    c.For<PortalEntityModel>().Use<PortalEntityModel>().SelectConstructor(() => new PortalEntityModel());
                }
            );
        }
    }
}