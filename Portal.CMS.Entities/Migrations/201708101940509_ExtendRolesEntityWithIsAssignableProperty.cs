namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendRolesEntityWithIsAssignableProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "IsAssignable", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Roles", "IsAssignable");
        }
    }
}