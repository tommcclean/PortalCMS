namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendAnalyticStorage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyticPageViews", "IPAddress", c => c.String(nullable: false));
            AddColumn("dbo.AnalyticPageViews", "UserAgent", c => c.String(nullable: false));
            AddColumn("dbo.AnalyticPostViews", "IPAddress", c => c.String(nullable: false));
            AddColumn("dbo.AnalyticPostViews", "UserAgent", c => c.String(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.AnalyticPostViews", "UserAgent");
            DropColumn("dbo.AnalyticPostViews", "IPAddress");
            DropColumn("dbo.AnalyticPageViews", "UserAgent");
            DropColumn("dbo.AnalyticPageViews", "IPAddress");
        }
    }
}