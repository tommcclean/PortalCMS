namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveThemeFromIndividualPages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pages", "ThemeId", "dbo.Themes");
            DropIndex("dbo.Pages", new[] { "ThemeId" });
            DropColumn("dbo.Pages", "ThemeId");
        }

        public override void Down()
        {
            AddColumn("dbo.Pages", "ThemeId", c => c.Int());
            CreateIndex("dbo.Pages", "ThemeId");
            AddForeignKey("dbo.Pages", "ThemeId", "dbo.Themes", "ThemeId");
        }
    }
}