namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialPortalCMSSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Copies",
                c => new
                {
                    CopyId = c.Int(nullable: false, identity: true),
                    CopyName = c.String(nullable: false),
                    CopyBody = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.CopyId);

            CreateTable(
                "dbo.Images",
                c => new
                {
                    ImageId = c.Int(nullable: false, identity: true),
                    ImageCategory = c.Int(nullable: false),
                    ImagePath = c.String(),
                })
                .PrimaryKey(t => t.ImageId);

            CreateTable(
                "dbo.MenuItems",
                c => new
                {
                    MenuItemId = c.Int(nullable: false, identity: true),
                    MenuId = c.Int(nullable: false),
                    LinkText = c.String(nullable: false),
                    LinkAction = c.String(nullable: false),
                    LinkController = c.String(nullable: false),
                    LinkArea = c.String(),
                })
                .PrimaryKey(t => t.MenuItemId)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);

            CreateTable(
                "dbo.Menus",
                c => new
                {
                    MenuId = c.Int(nullable: false, identity: true),
                    MenuName = c.String(nullable: false),
                })
                .PrimaryKey(t => t.MenuId);

            CreateTable(
                "dbo.PageComponentTypes",
                c => new
                {
                    PageComponentTypeId = c.Int(nullable: false, identity: true),
                    PageComponentTypeName = c.String(nullable: false),
                    PageComponentTypeCategory = c.Int(nullable: false),
                    PageComponentTypeDescription = c.String(nullable: false),
                    PageComponentBody = c.String(nullable: false),
                })
                .PrimaryKey(t => t.PageComponentTypeId);

            CreateTable(
                "dbo.Pages",
                c => new
                {
                    PageId = c.Int(nullable: false, identity: true),
                    PageName = c.String(nullable: false),
                    PageArea = c.String(),
                    PageController = c.String(nullable: false),
                    PageAction = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.PageId);

            CreateTable(
                "dbo.PageSections",
                c => new
                {
                    PageSectionId = c.Int(nullable: false, identity: true),
                    PageId = c.Int(nullable: false),
                    PageSectionTypeId = c.Int(nullable: false),
                    PageSectionBody = c.String(),
                })
                .PrimaryKey(t => t.PageSectionId)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .ForeignKey("dbo.PageSectionTypes", t => t.PageSectionTypeId, cascadeDelete: true)
                .Index(t => t.PageId)
                .Index(t => t.PageSectionTypeId);

            CreateTable(
                "dbo.PageSectionTypes",
                c => new
                {
                    PageSectionTypeId = c.Int(nullable: false, identity: true),
                    PageSectionTypeName = c.String(nullable: false),
                    PageSectionTypeBody = c.String(nullable: false),
                })
                .PrimaryKey(t => t.PageSectionTypeId);

            CreateTable(
                "dbo.PostCategories",
                c => new
                {
                    PostCategoryId = c.Int(nullable: false, identity: true),
                    PostCategoryName = c.String(nullable: false),
                })
                .PrimaryKey(t => t.PostCategoryId);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    PostId = c.Int(nullable: false, identity: true),
                    PostTitle = c.String(nullable: false),
                    PostDescription = c.String(nullable: false),
                    PostBody = c.String(nullable: false),
                    PostAuthorUserId = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    PostCategoryId = c.Int(nullable: false),
                    IsPublished = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.PostCategories", t => t.PostCategoryId, cascadeDelete: true)
                .Index(t => t.PostCategoryId);

            CreateTable(
                "dbo.PostComments",
                c => new
                {
                    PostCommentId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    PostId = c.Int(nullable: false),
                    CommentBody = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.PostCommentId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PostId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    EmailAddress = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    GivenName = c.String(nullable: false),
                    FamilyName = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.UserId);

            CreateTable(
                "dbo.UserRoles",
                c => new
                {
                    UserRoleId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    RoleId = c.Int(nullable: false, identity: true),
                    RoleName = c.String(nullable: false),
                })
                .PrimaryKey(t => t.RoleId);

            CreateTable(
                "dbo.PostImages",
                c => new
                {
                    PostImageId = c.Int(nullable: false, identity: true),
                    PostImageType = c.Int(nullable: false),
                    PostId = c.Int(nullable: false),
                    ImageId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.PostImageId)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ImageId);

            CreateTable(
                "dbo.Settings",
                c => new
                {
                    SettingId = c.Int(nullable: false, identity: true),
                    SettingName = c.String(nullable: false),
                    SettingValue = c.String(),
                })
                .PrimaryKey(t => t.SettingId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PostImages", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.PostComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PostComments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "PostCategoryId", "dbo.PostCategories");
            DropForeignKey("dbo.PageSections", "PageSectionTypeId", "dbo.PageSectionTypes");
            DropForeignKey("dbo.PageSections", "PageId", "dbo.Pages");
            DropForeignKey("dbo.MenuItems", "MenuId", "dbo.Menus");
            DropIndex("dbo.PostImages", new[] { "ImageId" });
            DropIndex("dbo.PostImages", new[] { "PostId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.PostComments", new[] { "PostId" });
            DropIndex("dbo.PostComments", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "PostCategoryId" });
            DropIndex("dbo.PageSections", new[] { "PageSectionTypeId" });
            DropIndex("dbo.PageSections", new[] { "PageId" });
            DropIndex("dbo.MenuItems", new[] { "MenuId" });
            DropTable("dbo.Settings");
            DropTable("dbo.PostImages");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.PostComments");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.PageSectionTypes");
            DropTable("dbo.PageSections");
            DropTable("dbo.Pages");
            DropTable("dbo.PageComponentTypes");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Images");
            DropTable("dbo.Copies");
        }
    }
}