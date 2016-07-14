namespace Portal.CMS.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAnalyticEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyticPageViews",
                c => new
                    {
                        AnalyticPageViewId = c.Int(nullable: false, identity: true),
                        Area = c.String(nullable: false),
                        Controller = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        ReferredUrl = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyticPageViewId);
            
            CreateTable(
                "dbo.AnalyticPostViews",
                c => new
                    {
                        AnalyticPageViewId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        ReferredUrl = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyticPageViewId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnalyticPostViews");
            DropTable("dbo.AnalyticPageViews");
        }
    }
}
