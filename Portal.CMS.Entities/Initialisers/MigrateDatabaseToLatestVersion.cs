namespace Portal.CMS.Entities.Initialisers
{
    using Portal.CMS.Entities.Migrations;
    using System.Data.Entity;

    internal class MigrateDatabaseToLatestVersion : MigrateDatabaseToLatestVersion<PortalDbContext, Configuration>
    {
        public override void InitializeDatabase(PortalDbContext context)
        {
            base.InitializeDatabase(context);

            Seed(context);
        }

        public virtual void Seed(PortalDbContext context)
        {
        }
    }
}