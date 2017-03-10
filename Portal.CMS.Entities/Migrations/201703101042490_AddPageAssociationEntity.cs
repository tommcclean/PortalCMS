namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPageAssociationEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageAssociations",
                c => new
                {
                    PageAssociationId = c.Int(nullable: false, identity: true),
                    PageId = c.Int(nullable: false),
                    PageSectionId = c.Int(),
                    PagePartialId = c.Int(),
                    PageAssociationOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PageAssociationId)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .ForeignKey("dbo.PagePartials", t => t.PagePartialId)
                .ForeignKey("dbo.PageSections", t => t.PageSectionId)
                .Index(t => t.PageId)
                .Index(t => t.PageSectionId)
                .Index(t => t.PagePartialId);

            CreateTable(
                "dbo.PagePartials",
                c => new
                {
                    PagePartialId = c.Int(nullable: false, identity: true),
                    RouteArea = c.String(),
                    RouteController = c.String(nullable: false),
                    RouteAction = c.String(nullable: false),
                })
                .PrimaryKey(t => t.PagePartialId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PageAssociations", "PageSectionId", "dbo.PageSections");
            DropForeignKey("dbo.PageAssociations", "PagePartialId", "dbo.PagePartials");
            DropForeignKey("dbo.PageAssociations", "PageId", "dbo.Pages");
            DropIndex("dbo.PageAssociations", new[] { "PagePartialId" });
            DropIndex("dbo.PageAssociations", new[] { "PageSectionId" });
            DropIndex("dbo.PageAssociations", new[] { "PageId" });
            DropTable("dbo.PagePartials");
            DropTable("dbo.PageAssociations");
        }
    }
}