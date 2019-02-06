namespace Portal.CMS.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeychangedtIDandRoeNamechangedtoNameinRoletable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItemRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageAssociationRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PostRoles", "RoleId", "dbo.Roles");
            DropPrimaryKey("dbo.Roles");
            AddColumn("dbo.Roles", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Roles", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Roles", "Id");
            AddForeignKey("dbo.MenuItemRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PageRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PageAssociationRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            DropColumn("dbo.Roles", "RoleId");
            DropColumn("dbo.Roles", "RoleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "RoleName", c => c.String(nullable: false));
            AddColumn("dbo.Roles", "RoleId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PostRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageAssociationRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MenuItemRoles", "RoleId", "dbo.Roles");
            DropPrimaryKey("dbo.Roles");
            DropColumn("dbo.Roles", "Name");
            DropColumn("dbo.Roles", "Id");
            AddPrimaryKey("dbo.Roles", "RoleId");
            AddForeignKey("dbo.PostRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.PageAssociationRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.PageRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.MenuItemRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}
