namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendUserTokenEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserTokens", "DateRedeemed", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("dbo.UserTokens", "DateRedeemed", c => c.DateTime(nullable: false));
        }
    }
}