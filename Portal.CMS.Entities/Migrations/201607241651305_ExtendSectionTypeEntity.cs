namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendSectionTypeEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageSectionTypes", "PageSectionTypeCategory", c => c.Int(nullable: false));
            AddColumn("dbo.PageSectionTypes", "PageSectionTypeOrder", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PageSectionTypes", "PageSectionTypeOrder");
            DropColumn("dbo.PageSectionTypes", "PageSectionTypeCategory");
        }
    }
}