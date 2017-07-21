namespace Portal.CMS.Entities.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class StripWebsiteAddressFromUploadedContent : DbMigration
    {
        private readonly PortalEntityModel _context = new PortalEntityModel();

        public override void Up()
        {
            var uploadedImages = _context.Images.ToList();

            foreach (var image in uploadedImages)
            {
                image.ImagePath = StripWebsiteAddress(image.ImagePath);
            }

            var uploadedFonts = _context.Fonts.ToList();

            foreach (var font in uploadedFonts)
            {
                font.FontPath = StripWebsiteAddress(font.FontPath);
            }

            _context.SaveChanges();
        }

        public override void Down()
        {
        }

        private string StripWebsiteAddress(string filePath)
        {
            var startPosition = filePath.ToLower().IndexOf("/areas");

            if (startPosition < 1)
            {
                return filePath;
            }

            filePath = filePath.Substring(startPosition);

            return filePath;
        }
    }
}