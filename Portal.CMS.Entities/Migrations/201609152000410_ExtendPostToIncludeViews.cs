namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendPostToIncludeViews : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AnalyticPostViews", "PostId");
            AddForeignKey("dbo.AnalyticPostViews", "PostId", "dbo.Posts", "PostId", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AnalyticPostViews", "PostId", "dbo.Posts");
            DropIndex("dbo.AnalyticPostViews", new[] { "PostId" });
        }
    }
}