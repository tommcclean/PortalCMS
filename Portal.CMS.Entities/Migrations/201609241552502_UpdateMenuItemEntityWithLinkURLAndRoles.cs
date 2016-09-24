namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateMenuItemEntityWithLinkURLAndRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemRoles",
                c => new
                {
                    MenuItemRoleId = c.Int(nullable: false, identity: true),
                    MenuItemId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MenuItemRoleId)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.MenuItemId)
                .Index(t => t.RoleId);

            AddColumn("dbo.MenuItems", "LinkURL", c => c.String(nullable: false));
            DropColumn("dbo.MenuItems", "LinkAction");
            DropColumn("dbo.MenuItems", "LinkController");
            DropColumn("dbo.MenuItems", "LinkArea");
        }

        public override void Down()
        {
            AddColumn("dbo.MenuItems", "LinkArea", c => c.String());
            AddColumn("dbo.MenuItems", "LinkController", c => c.String(nullable: false));
            AddColumn("dbo.MenuItems", "LinkAction", c => c.String(nullable: false));
            DropForeignKey("dbo.MenuItemRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MenuItemRoles", "MenuItemId", "dbo.MenuItems");
            DropIndex("dbo.MenuItemRoles", new[] { "RoleId" });
            DropIndex("dbo.MenuItemRoles", new[] { "MenuItemId" });
            DropColumn("dbo.MenuItems", "LinkURL");
            DropTable("dbo.MenuItemRoles");
        }
    }
}