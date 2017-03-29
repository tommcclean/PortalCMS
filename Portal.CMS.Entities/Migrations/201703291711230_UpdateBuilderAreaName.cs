namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateBuilderAreaName : DbMigration
    {
        private readonly PortalEntityModel _context = new PortalEntityModel();

        public override void Up()
        {
            foreach (var image in _context.Images)
                image.ImagePath = image.ImagePath.Replace("Areas/Builder/", "Areas/PageBuilder/");

            foreach (var pageSection in _context.PageSections)
                pageSection.PageSectionBody = pageSection.PageSectionBody.Replace("Areas/Builder/", "Areas/PageBuilder/");

            foreach (var pageSection in _context.PageSectionBackups)
                pageSection.PageSectionBody = pageSection.PageSectionBody.Replace("Areas/Builder/", "Areas/PageBuilder/");

            foreach (var post in _context.Posts)
                post.PostBody = post.PostBody.Replace("Areas/Builder/", "Areas/PageBuilder/");

            _context.SaveChanges();
        }

        public override void Down()
        {
            foreach (var image in _context.Images)
                image.ImagePath = image.ImagePath.Replace("Areas/PageBuilder/", "Areas/Builder/");

            foreach (var pageSection in _context.PageSections)
                pageSection.PageSectionBody = pageSection.PageSectionBody.Replace("Areas/PageBuilder/", "Areas/Builder/");

            foreach (var pageSection in _context.PageSectionBackups)
                pageSection.PageSectionBody = pageSection.PageSectionBody.Replace("Areas/PageBuilder/", "Areas/Builder/");

            foreach (var post in _context.Posts)
                post.PostBody = post.PostBody.Replace("Areas/PageBuilder/", "Areas/Builder/");

            _context.SaveChanges();
        }
    }
}