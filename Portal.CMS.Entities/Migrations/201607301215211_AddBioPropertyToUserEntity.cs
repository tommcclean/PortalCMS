namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddBioPropertyToUserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Bio", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "Bio");
        }
    }
}