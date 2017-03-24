namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AvoidReservedTypeNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Copies", newName: "CopyItems");
            RenameTable(name: "dbo.Menus", newName: "MenuSystems");
            RenameTable(name: "dbo.Themes", newName: "CustomThemes");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.CustomThemes", newName: "Themes");
            RenameTable(name: "dbo.MenuSystems", newName: "Menus");
            RenameTable(name: "dbo.CopyItems", newName: "Copies");
        }
    }
}