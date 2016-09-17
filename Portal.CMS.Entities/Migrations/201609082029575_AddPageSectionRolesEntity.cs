namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPageSectionRolesEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageSectionRoles",
                c => new
                {
                    PageSectionRoleId = c.Int(nullable: false, identity: true),
                    PageSectionId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PageSectionRoleId)
                .ForeignKey("dbo.PageSections", t => t.PageSectionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PageSectionId)
                .Index(t => t.RoleId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PageSectionRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageSectionRoles", "PageSectionId", "dbo.PageSections");
            DropIndex("dbo.PageSectionRoles", new[] { "RoleId" });
            DropIndex("dbo.PageSectionRoles", new[] { "PageSectionId" });
            DropTable("dbo.PageSectionRoles");
        }
    }
}