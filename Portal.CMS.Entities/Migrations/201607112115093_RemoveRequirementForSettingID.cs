namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveRequirementForSettingID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Settings", "SettingValue", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Settings", "SettingValue", c => c.String(nullable: false));
        }
    }
}