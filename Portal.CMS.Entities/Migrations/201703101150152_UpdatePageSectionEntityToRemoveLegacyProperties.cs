namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatePageSectionEntityToRemoveLegacyProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PageSections", "PageId", "dbo.Pages");
            DropIndex("dbo.PageSections", new[] { "PageId" });
            DropColumn("dbo.PageSections", "PageId");
            DropColumn("dbo.PageSections", "PageSectionOrder");
        }

        public override void Down()
        {
            AddColumn("dbo.PageSections", "PageSectionOrder", c => c.Int(nullable: false));
            AddColumn("dbo.PageSections", "PageId", c => c.Int(nullable: false));
            CreateIndex("dbo.PageSections", "PageId");
            AddForeignKey("dbo.PageSections", "PageId", "dbo.Pages", "PageId", cascadeDelete: true);
        }
    }
}