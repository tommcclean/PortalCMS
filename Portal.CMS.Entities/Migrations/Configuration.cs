namespace Portal.CMS.Entities.Migrations
{
    using Seed;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Portal.CMS.Entities.PortalEntityModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Portal.CMS.Entities.PortalEntityModel context)
        {
            RoleSeed.Seed(context);
            SettingSeed.Seed(context);
            MenuSeed.Seed(context);
            PageSectionTypeSeed.Seed(context);
            PageComponentTypeSeed.Seed(context);

            context.SaveChanges();

            PageSeed.Seed(context);
        }
    }
}
