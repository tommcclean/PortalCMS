namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPostRolesEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostRoles",
                c => new
                {
                    PostRoleId = c.Int(nullable: false, identity: true),
                    PostId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PostRoleId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.RoleId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PostRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PostRoles", "PostId", "dbo.Posts");
            DropIndex("dbo.PostRoles", new[] { "RoleId" });
            DropIndex("dbo.PostRoles", new[] { "PostId" });
            DropTable("dbo.PostRoles");
        }
    }
}