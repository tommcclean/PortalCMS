namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveRequirementForReferralAnalytic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnalyticPageViews", "ReferredUrl", c => c.String());
            AlterColumn("dbo.AnalyticPostViews", "ReferredUrl", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.AnalyticPostViews", "ReferredUrl", c => c.String(nullable: false));
            AlterColumn("dbo.AnalyticPageViews", "ReferredUrl", c => c.String(nullable: false));
        }
    }
}