namespace Portal.CMS.Entities.Initialisers
{
    using System.Data.Entity;
    using Portal.CMS.Entities.Migrations;

    internal class MigrateDatabaseToLatestVersion : MigrateDatabaseToLatestVersion<PortalEntityModel, Configuration>
    {
        public override void InitializeDatabase(PortalEntityModel context)
        {
            base.InitializeDatabase(context);

            this.Seed(context);
        }

        public virtual void Seed(PortalEntityModel context)
        {
        }
    }
}