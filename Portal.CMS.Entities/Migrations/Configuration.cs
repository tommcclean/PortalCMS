namespace Portal.CMS.Entities.Migrations
{
    using Seed;
    using System.Data.Entity.Migrations;

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
            PostCategorySeed.Seed(context);
            CopySeed.Seed(context);
            PageSectionTypeSeed.Seed(context);
            PageComponentTypeSeed.Seed(context);

            context.SaveChanges();

            PageSeed.Seed(context);
        }
    }
}