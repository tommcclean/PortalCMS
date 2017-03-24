namespace Portal.CMS.Entities.Initialisers
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    public class DropAndMigrateDatabaseToLatestVersion<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext> where TContext : DbContext, new() where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        private readonly TMigrationsConfiguration config;

        public DropAndMigrateDatabaseToLatestVersion()
        {
            config = new TMigrationsConfiguration();
        }

        public void InitializeDatabase(TContext context)
        {
            context.Database.Delete();

            var migrator = new DbMigrator(config);
            migrator.Update();

            Seed(context);
        }

        public virtual void Seed(TContext context)
        {
        }
    }
}