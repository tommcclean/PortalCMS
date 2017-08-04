using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Generic
{
    public interface IImageService
    {
        Task<Image> GetAsync(int imageId);

        Task<List<Image>> GetAsync();

        Task<int> CreateAsync(string imageFilePath, ImageCategory imageCategory);

        Task DeleteAsync(int imageId);
    }

    public class ImageService : IImageService
    {
        #region Dependencies

        readonly PortalEntityModel _context;

        public ImageService(PortalEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        public async Task<Image> GetAsync(int imageId)
        {
            var results = await _context.Images.SingleOrDefaultAsync(x => x.ImageId == imageId);

            return results;
        }

        public async Task<List<Image>> GetAsync()
        {
            var results = await _context.Images.OrderByDescending(x => x.ImageId).ToListAsync();

            return results;
        }

        public async Task<int> CreateAsync(string imageFilePath, ImageCategory imageCategory)
        {
            var image = new Image
            {
                ImagePath = imageFilePath,
                ImageCategory = imageCategory
            };

            _context.Images.Add(image);

            await _context.SaveChangesAsync();

            return image.ImageId;
        }

        public async Task DeleteAsync(int imageId)
        {
            var image = await _context.Images.SingleOrDefaultAsync(x => x.ImageId == imageId);
            if (image == null) return;

            _context.Images.Remove(image);

            await _context.SaveChangesAsync();
        }
    }
}