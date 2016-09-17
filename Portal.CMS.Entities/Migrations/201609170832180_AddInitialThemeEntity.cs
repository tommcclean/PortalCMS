namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddInitialThemeEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Themes",
                c => new
                {
                    ThemeId = c.Int(nullable: false, identity: true),
                    ThemeName = c.String(nullable: false),
                    IsDefault = c.Boolean(nullable: false),
                    TitleFontId = c.Int(),
                    TextFontId = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ThemeId)
                .ForeignKey("dbo.Fonts", t => t.TextFontId)
                .ForeignKey("dbo.Fonts", t => t.TitleFontId)
                .Index(t => t.TitleFontId)
                .Index(t => t.TextFontId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Themes", "TitleFontId", "dbo.Fonts");
            DropForeignKey("dbo.Themes", "TextFontId", "dbo.Fonts");
            DropIndex("dbo.Themes", new[] { "TextFontId" });
            DropIndex("dbo.Themes", new[] { "TitleFontId" });
            DropTable("dbo.Themes");
        }
    }
}