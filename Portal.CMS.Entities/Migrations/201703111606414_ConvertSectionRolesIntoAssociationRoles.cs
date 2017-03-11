namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ConvertSectionRolesIntoAssociationRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PageSectionRoles", "PageSectionId", "dbo.PageSections");
            DropForeignKey("dbo.PageSectionRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.PageSectionRoles", new[] { "PageSectionId" });
            DropIndex("dbo.PageSectionRoles", new[] { "RoleId" });
            CreateTable(
                "dbo.PageAssociationRoles",
                c => new
                {
                    PageAssociationRoleId = c.Int(nullable: false, identity: true),
                    PageAssociationId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PageAssociationRoleId)
                .ForeignKey("dbo.PageAssociations", t => t.PageAssociationId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PageAssociationId)
                .Index(t => t.RoleId);

            DropTable("dbo.PageSectionRoles");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.PageSectionRoles",
                c => new
                {
                    PageSectionRoleId = c.Int(nullable: false, identity: true),
                    PageSectionId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PageSectionRoleId);

            DropForeignKey("dbo.PageAssociationRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageAssociationRoles", "PageAssociationId", "dbo.PageAssociations");
            DropIndex("dbo.PageAssociationRoles", new[] { "RoleId" });
            DropIndex("dbo.PageAssociationRoles", new[] { "PageAssociationId" });
            DropTable("dbo.PageAssociationRoles");
            CreateIndex("dbo.PageSectionRoles", "RoleId");
            CreateIndex("dbo.PageSectionRoles", "PageSectionId");
            AddForeignKey("dbo.PageSectionRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.PageSectionRoles", "PageSectionId", "dbo.PageSections", "PageSectionId", cascadeDelete: true);
        }
    }
}