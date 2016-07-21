namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddAvatarPathToUserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarImagePath", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "AvatarImagePath");
        }
    }
}