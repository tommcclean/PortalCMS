namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPageSectionBackupEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageSectionBackups",
                c => new
                {
                    PageSectionBackupId = c.Int(nullable: false, identity: true),
                    PageSectionId = c.Int(nullable: false),
                    PageSectionBody = c.String(),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.PageSectionBackupId)
                .ForeignKey("dbo.PageSections", t => t.PageSectionId, cascadeDelete: true)
                .Index(t => t.PageSectionId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PageSectionBackups", "PageSectionId", "dbo.PageSections");
            DropIndex("dbo.PageSectionBackups", new[] { "PageSectionId" });
            DropTable("dbo.PageSectionBackups");
        }
    }
}