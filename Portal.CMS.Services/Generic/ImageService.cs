using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Generic;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Portal.CMS.Services.Generic
{
    public interface IImageService
    {
        List<Image> Get();

        int Create(string imageFilePath, ImageCategory imageCategory);

        void Delete(int imageId);
    }

    public class ImageService : IImageService
    {
        #region Dependencies

        private readonly PortalEntityModel _context;

        public ImageService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public List<Image> Get()
        {
            var results = _context.Images.OrderByDescending(x => x.ImageId).ToList();

            return results;
        }

        public int Create(string imageFilePath, ImageCategory imageCategory)
        {
            var image = new Image()
            {
                ImagePath = imageFilePath,
                ImageCategory = imageCategory
            };

            _context.Images.Add(image);

            _context.SaveChanges();

            return image.ImageId;
        }

        public void Delete(int imageId)
        {
            var image = _context.Images.FirstOrDefault(x => x.ImageId == imageId);

            if (image == null)
                return;

            if (File.Exists(image.ImagePath))
                File.Delete(image.ImagePath);

            _context.Images.Remove(image);

            _context.SaveChanges();
        }
    }
}