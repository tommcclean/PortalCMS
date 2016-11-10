namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CleanupPageBuilderEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PageSections", "PageSectionTypeId", "dbo.PageSectionTypes");
            DropIndex("dbo.PageSections", new[] { "PageSectionTypeId" });
            DropColumn("dbo.PageComponentTypes", "PageComponentTypeDescription");
            DropColumn("dbo.PageSections", "PageSectionTypeId");
        }

        public override void Down()
        {
            AddColumn("dbo.PageSections", "PageSectionTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.PageComponentTypes", "PageComponentTypeDescription", c => c.String(nullable: false));
            CreateIndex("dbo.PageSections", "PageSectionTypeId");
            AddForeignKey("dbo.PageSections", "PageSectionTypeId", "dbo.PageSectionTypes", "PageSectionTypeId", cascadeDelete: true);
        }
    }
}