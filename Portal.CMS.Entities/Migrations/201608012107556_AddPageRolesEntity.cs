namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPageRolesEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageRoles",
                c => new
                {
                    PageRoleId = c.Int(nullable: false, identity: true),
                    PageId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PageRoleId)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PageId)
                .Index(t => t.RoleId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PageRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageRoles", "PageId", "dbo.Pages");
            DropIndex("dbo.PageRoles", new[] { "RoleId" });
            DropIndex("dbo.PageRoles", new[] { "PageId" });
            DropTable("dbo.PageRoles");
        }
    }
}