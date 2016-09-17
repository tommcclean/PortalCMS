namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFontEntityForTheming : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fonts",
                c => new
                {
                    FontId = c.Int(nullable: false, identity: true),
                    FontName = c.String(nullable: false),
                    FontType = c.String(nullable: false),
                    FontPath = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.FontId);
        }

        public override void Down()
        {
            DropTable("dbo.Fonts");
        }
    }
}