namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendThemePropertiesWithFontSizes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Themes", "TitleLargeFontSize", c => c.Int(nullable: false));
            AddColumn("dbo.Themes", "TitleMediumFontSize", c => c.Int(nullable: false));
            AddColumn("dbo.Themes", "TitleSmallFontSize", c => c.Int(nullable: false));
            AddColumn("dbo.Themes", "TitleTinyFontSize", c => c.Int(nullable: false));
            AddColumn("dbo.Themes", "TextStandardFontSize", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Themes", "TextStandardFontSize");
            DropColumn("dbo.Themes", "TitleTinyFontSize");
            DropColumn("dbo.Themes", "TitleSmallFontSize");
            DropColumn("dbo.Themes", "TitleMediumFontSize");
            DropColumn("dbo.Themes", "TitleLargeFontSize");
        }
    }
}