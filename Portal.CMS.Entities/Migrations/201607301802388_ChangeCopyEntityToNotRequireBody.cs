namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeCopyEntityToNotRequireBody : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Copies", "CopyBody", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Copies", "CopyBody", c => c.String(nullable: false));
        }
    }
}