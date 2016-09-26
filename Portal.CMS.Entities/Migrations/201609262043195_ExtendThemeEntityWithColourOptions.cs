namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendThemeEntityWithColourOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Themes", "PageBackgroundColour", c => c.String(nullable: false));
            AddColumn("dbo.Themes", "MenuBackgroundColour", c => c.String(nullable: false));
            AddColumn("dbo.Themes", "MenuTextColour", c => c.String(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Themes", "MenuTextColour");
            DropColumn("dbo.Themes", "MenuBackgroundColour");
            DropColumn("dbo.Themes", "PageBackgroundColour");
        }
    }
}