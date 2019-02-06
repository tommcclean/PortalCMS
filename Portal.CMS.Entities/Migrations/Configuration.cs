namespace Portal.CMS.Entities.Migrations
{
    using Seed;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Portal.CMS.Entities.PortalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Portal.CMS.Entities.PortalDbContext context)
        {
            ThemeSeed.Seed(context);
            RoleSeed.Seed(context);
            SettingSeed.Seed(context);
            PostCategorySeed.Seed(context);
            PageSectionTypeSeed.Seed(context);
            PageComponentTypeSeed.Seed(context);
            MediaSeed.Seed(context);
            CopySeed.Seed(context);
            context.SaveChanges();

            MenuSeed.Seed(context);
            PageSeed.Seed(context);
            PostSeed.Seed(context);
        }
    }
}