namespace Portal.CMS.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendPostsFurtherToSupportCategoryId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "PostCategory_PostCategoryId", newName: "PostCategoryId");
            RenameIndex(table: "dbo.Posts", name: "IX_PostCategory_PostCategoryId", newName: "IX_PostCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_PostCategoryId", newName: "IX_PostCategory_PostCategoryId");
            RenameColumn(table: "dbo.Posts", name: "PostCategoryId", newName: "PostCategory_PostCategoryId");
        }
    }
}
