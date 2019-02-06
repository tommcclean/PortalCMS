namespace Portal.CMS.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Columnnamechangeforkeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalyticPageViews",
                c => new
                    {
                        AnalyticPageViewId = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(nullable: false),
                        UserAgent = c.String(nullable: false),
                        Controller = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Area = c.String(),
                        ReferredUrl = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyticPageViewId);
            
            CreateTable(
                "dbo.AnalyticPostViews",
                c => new
                    {
                        AnalyticPageViewId = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(nullable: false),
                        UserAgent = c.String(nullable: false),
                        PostId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        ReferredUrl = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyticPageViewId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.CopyItems",
                c => new
                    {
                        CopyId = c.Int(nullable: false, identity: true),
                        CopyName = c.String(nullable: false),
                        CopyBody = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CopyId);
            
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
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        MenuItemId = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        LinkText = c.String(nullable: false),
                        LinkIcon = c.String(),
                        LinkURL = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MenuItemId)
                .ForeignKey("dbo.MenuSystems", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.MenuSystems",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        IsAssignable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.PageAssociations",
                c => new
                    {
                        PageAssociationId = c.Int(nullable: false, identity: true),
                        PageId = c.Int(nullable: false),
                        PageSectionId = c.Int(),
                        PagePartialId = c.Int(),
                        PageAssociationOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PageAssociationId)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .ForeignKey("dbo.PagePartials", t => t.PagePartialId)
                .ForeignKey("dbo.PageSections", t => t.PageSectionId)
                .Index(t => t.PageId)
                .Index(t => t.PageSectionId)
                .Index(t => t.PagePartialId);
            
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
            
            CreateTable(
                "dbo.PagePartials",
                c => new
                    {
                        PagePartialId = c.Int(nullable: false, identity: true),
                        RouteArea = c.String(),
                        RouteController = c.String(nullable: false),
                        RouteAction = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PagePartialId);
            
            CreateTable(
                "dbo.PageSections",
                c => new
                    {
                        PageSectionId = c.Int(nullable: false, identity: true),
                        PageSectionBody = c.String(),
                    })
                .PrimaryKey(t => t.PageSectionId);
            
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
            
            CreateTable(
                "dbo.PageComponentTypes",
                c => new
                    {
                        PageComponentTypeId = c.Int(nullable: false, identity: true),
                        PageComponentTypeName = c.String(nullable: false),
                        PageComponentTypeCategory = c.Int(nullable: false),
                        PageComponentBody = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PageComponentTypeId);
            
            CreateTable(
                "dbo.PageSectionTypes",
                c => new
                    {
                        PageSectionTypeId = c.Int(nullable: false, identity: true),
                        PageSectionTypeName = c.String(nullable: false),
                        PageSectionTypeBody = c.String(nullable: false),
                        PageSectionTypeCategory = c.Int(nullable: false),
                        PageSectionTypeOrder = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        GivenName = c.String(nullable: false),
                        FamilyName = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        AvatarImagePath = c.String(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        SettingName = c.String(nullable: false),
                        SettingValue = c.String(),
                    })
                .PrimaryKey(t => t.SettingId);
            
            CreateTable(
                "dbo.CustomThemes",
                c => new
                    {
                        ThemeId = c.Int(nullable: false, identity: true),
                        ThemeName = c.String(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        TitleFontId = c.Int(),
                        TextFontId = c.Int(),
                        TitleLargeFontSize = c.Int(nullable: false),
                        TitleMediumFontSize = c.Int(nullable: false),
                        TitleSmallFontSize = c.Int(nullable: false),
                        TitleTinyFontSize = c.Int(nullable: false),
                        TextStandardFontSize = c.Int(nullable: false),
                        PageBackgroundColour = c.String(nullable: false),
                        MenuBackgroundColour = c.String(nullable: false),
                        MenuTextColour = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ThemeId)
                .ForeignKey("dbo.Fonts", t => t.TextFontId)
                .ForeignKey("dbo.Fonts", t => t.TitleFontId)
                .Index(t => t.TitleFontId)
                .Index(t => t.TextFontId);
            
            CreateTable(
                "dbo.UserTokens",
                c => new
                    {
                        UserTokenId = c.Int(nullable: false, identity: true),
                        UserTokenType = c.Int(nullable: false),
                        Token = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateRedeemed = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserTokenId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomThemes", "TitleFontId", "dbo.Fonts");
            DropForeignKey("dbo.CustomThemes", "TextFontId", "dbo.Fonts");
            DropForeignKey("dbo.AnalyticPostViews", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PostRoles", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostImages", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.PostComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PostComments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "PostCategoryId", "dbo.PostCategories");
            DropForeignKey("dbo.PageAssociationRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageAssociationRoles", "PageAssociationId", "dbo.PageAssociations");
            DropForeignKey("dbo.PageAssociations", "PageSectionId", "dbo.PageSections");
            DropForeignKey("dbo.PageSectionBackups", "PageSectionId", "dbo.PageSections");
            DropForeignKey("dbo.PageAssociations", "PagePartialId", "dbo.PagePartials");
            DropForeignKey("dbo.PageAssociations", "PageId", "dbo.Pages");
            DropForeignKey("dbo.PageRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PageRoles", "PageId", "dbo.Pages");
            DropForeignKey("dbo.MenuItemRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MenuItemRoles", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "MenuId", "dbo.MenuSystems");
            DropIndex("dbo.UserTokens", new[] { "UserId" });
            DropIndex("dbo.CustomThemes", new[] { "TextFontId" });
            DropIndex("dbo.CustomThemes", new[] { "TitleFontId" });
            DropIndex("dbo.PostRoles", new[] { "RoleId" });
            DropIndex("dbo.PostRoles", new[] { "PostId" });
            DropIndex("dbo.PostImages", new[] { "ImageId" });
            DropIndex("dbo.PostImages", new[] { "PostId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.PostComments", new[] { "PostId" });
            DropIndex("dbo.PostComments", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "PostCategoryId" });
            DropIndex("dbo.PageSectionBackups", new[] { "PageSectionId" });
            DropIndex("dbo.PageRoles", new[] { "RoleId" });
            DropIndex("dbo.PageRoles", new[] { "PageId" });
            DropIndex("dbo.PageAssociations", new[] { "PagePartialId" });
            DropIndex("dbo.PageAssociations", new[] { "PageSectionId" });
            DropIndex("dbo.PageAssociations", new[] { "PageId" });
            DropIndex("dbo.PageAssociationRoles", new[] { "RoleId" });
            DropIndex("dbo.PageAssociationRoles", new[] { "PageAssociationId" });
            DropIndex("dbo.MenuItems", new[] { "MenuId" });
            DropIndex("dbo.MenuItemRoles", new[] { "RoleId" });
            DropIndex("dbo.MenuItemRoles", new[] { "MenuItemId" });
            DropIndex("dbo.AnalyticPostViews", new[] { "PostId" });
            DropTable("dbo.UserTokens");
            DropTable("dbo.CustomThemes");
            DropTable("dbo.Settings");
            DropTable("dbo.PostRoles");
            DropTable("dbo.PostImages");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.PostComments");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.PageSectionTypes");
            DropTable("dbo.PageComponentTypes");
            DropTable("dbo.PageSectionBackups");
            DropTable("dbo.PageSections");
            DropTable("dbo.PagePartials");
            DropTable("dbo.PageRoles");
            DropTable("dbo.Pages");
            DropTable("dbo.PageAssociations");
            DropTable("dbo.PageAssociationRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.MenuSystems");
            DropTable("dbo.MenuItems");
            DropTable("dbo.MenuItemRoles");
            DropTable("dbo.Images");
            DropTable("dbo.Fonts");
            DropTable("dbo.CopyItems");
            DropTable("dbo.AnalyticPostViews");
            DropTable("dbo.AnalyticPageViews");
        }
    }
}
