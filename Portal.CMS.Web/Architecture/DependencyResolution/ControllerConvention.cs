namespace Portal.CMS.Web.DependencyResolution
{
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using StructureMap.Pipeline;
    using StructureMap.TypeRules;
    using System;
    using System.Web.Mvc;

    public class ControllerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
            {
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}