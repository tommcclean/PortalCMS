namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedPageAssociationEntity : DbMigration
    {
        private readonly PortalEntityModel _context = new PortalEntityModel();

        public override void Up()
        {
            // TRANSITION: Section Detail over to new Page Association Entity
            _context.Database.ExecuteSqlCommand("INSERT INTO PageAssociations (PageId, PageSectionId, PageAssociationOrder) SELECT PageId, PageSectionId, PageSectionOrder FROM PageSections");
        }

        public override void Down()
        {
        }
    }
}