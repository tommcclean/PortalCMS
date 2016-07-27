namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserTokenEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTokens",
                c => new
                {
                    UserTokenId = c.Int(nullable: false, identity: true),
                    UserTokenType = c.Int(nullable: false),
                    Token = c.String(nullable: false),
                    UserId = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    DateRedeemed = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.UserTokenId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserTokens", "UserId", "dbo.Users");
            DropIndex("dbo.UserTokens", new[] { "UserId" });
            DropTable("dbo.UserTokens");
        }
    }
}