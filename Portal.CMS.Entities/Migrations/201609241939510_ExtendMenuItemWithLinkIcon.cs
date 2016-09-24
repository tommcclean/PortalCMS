namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ExtendMenuItemWithLinkIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "LinkIcon", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.MenuItems", "LinkIcon");
        }
    }
}