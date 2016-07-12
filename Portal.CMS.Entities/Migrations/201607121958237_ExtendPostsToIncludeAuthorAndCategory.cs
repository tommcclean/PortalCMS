namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendPostsToIncludeAuthorAndCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostCategories",
                c => new
                {
                    PostCategoryId = c.Int(nullable: false, identity: true),
                    PostCategoryName = c.String(nullable: false),
                })
                .PrimaryKey(t => t.PostCategoryId);

            AddColumn("dbo.Posts", "PostAuthorUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "PostCategory_PostCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "PostCategory_PostCategoryId");
            AddForeignKey("dbo.Posts", "PostCategory_PostCategoryId", "dbo.PostCategories", "PostCategoryId", cascadeDelete: true);
            DropColumn("dbo.Posts", "PostCategory");
        }

        public override void Down()
        {
            AddColumn("dbo.Posts", "PostCategory", c => c.Int(nullable: false));
            DropForeignKey("dbo.Posts", "PostCategory_PostCategoryId", "dbo.PostCategories");
            DropIndex("dbo.Posts", new[] { "PostCategory_PostCategoryId" });
            DropColumn("dbo.Posts", "PostCategory_PostCategoryId");
            DropColumn("dbo.Posts", "PostAuthorUserId");
            DropTable("dbo.PostCategories");
        }
    }
}