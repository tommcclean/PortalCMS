namespace Portal.CMS.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Portal.CMS.Entities.Entities.PageBuilder;

    public partial class SeedPageAssociationEntity : DbMigration
    {
        private readonly PortalEntityModel _context = new PortalEntityModel();

        public override void Up()
        {
            // TRANSITION: Section Detail over to new Page Association Entity
            foreach (var section in _context.PageSections)
            {
                _context.PageAssociations.Add(new PageAssociation
                {
                    PageId = section.PageId,
                    PageSectionId = section.PageSectionId,
                    PageAssociationOrder = section.PageSectionOrder,
                });
            }

            _context.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
