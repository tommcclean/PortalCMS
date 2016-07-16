namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPageSectionOrderToPageSections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageSections", "PageSectionOrder", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PageSections", "PageSectionOrder");
        }
    }
}